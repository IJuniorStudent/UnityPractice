using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : RaycastTarget
{
    private const int MinUnitsToCreateBase = 2;
    
    [SerializeField] private FlagCreator _flagCreator;
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private int _scanInterval = 3;
    
    private StateMachine<ResourceBase> _stateMachine;
    private ResourceCreator _resourceCreator;
    private WaitForSeconds _scanDelay;
    
    public event Action<bool> SelectStateChanged;
    public event Action<ResourceBase, Unit, Vector3> UnitCreatedResourceBase;
    public event Action<int> UnitCountChanged;
    
    private void Awake()
    {
        _scanDelay = new WaitForSeconds(_scanInterval);
        
        var factory = new ResourceBaseStateFactory(this, _resourceStorage);
        _stateMachine = new StateMachine<ResourceBase>(factory.CreateStates());
        _stateMachine.ChangeState<UnitCreateState>();
    }
    
    private void Start()
    {
        StartCoroutine(ScanResources());
    }
    
    private void OnEnable()
    {
        _resourceScanner.ResourcesCollected += OnResourcesScanned;
        _unitCreator.Created += Link;
    }
    
    private void OnDisable()
    {
        _resourceScanner.ResourcesCollected -= OnResourcesScanned;
        _unitCreator.Created -= Link;
    }
    
    public void Initialize(ResourceCreator resourceCreator, ResourceBaseFoundation flagPrefab)
    {
        _resourceCreator = resourceCreator;
        _flagCreator.Initialize(flagPrefab);
    }
    
    public void SetSelected(bool isSelected)
    {
        SelectStateChanged?.Invoke(isSelected);
    }
    
    public void TryScheduleBaseCreation(Vector3 position)
    {
        if (_flagCreator.IsWorkActive)
        {
            _flagCreator.UpdatePosition(position);
            return;
        }
        
        if (_unitCreator.TotalCount < MinUnitsToCreateBase)
            return;
        
        _flagCreator.Activate(position);
        _stateMachine.ChangeState<ResourceBaseCreateState>();
    }
    
    public void CreateInitialUnits(int count)
    {
        _unitCreator.Create(count);
    }
    
    public void TryCreateNewUnit(int resourceCost)
    {
        if (_resourceStorage.TrySpend(resourceCost))
            _unitCreator.Create(1);
    }
    
    public void TryCreateNewBase(int resourceCost)
    {
        if (_unitCreator.FreeCount == 0)
            return;
        
        if (_unitCreator.TryGetFreeObject(out Unit unit) == false)
            return;
        
        if (_unitCreator.TryReserve(unit) == false)
            return;
        
        if (_resourceStorage.TrySpend(resourceCost) == false)
        {
            _unitCreator.TryFree(unit);
            return;
        }
        
        _flagCreator.SetWorker(unit);
        _stateMachine.ChangeState<UnitCreateState>();
    }
    
    public void Link(Unit unit)
    {
        _unitCreator.Link(unit);
        _unitCreator.Relocate(unit);
        UnitCountChanged?.Invoke(_unitCreator.TotalCount);
        
        unit.ResourceCollected += OnUnitCollectedResource;
        unit.ResourceStored += OnUnitStoredResource;
        unit.ResourceBaseCreated += OnUnitCreatedResourceBase;
        
        unit.SetHomeStorage(_resourceStorage);
    }
    
    public void Unlink(Unit unit)
    {
        unit.ResourceCollected -= OnUnitCollectedResource;
        unit.ResourceStored -= OnUnitStoredResource;
        unit.ResourceBaseCreated -= OnUnitCreatedResourceBase;
        
        _unitCreator.Unlink(unit);
        UnitCountChanged?.Invoke(_unitCreator.TotalCount);
    }
    
    private IEnumerator ScanResources()
    {
        while (enabled)
        {
            yield return _scanDelay;
            _resourceScanner.Scan();
        }
    }
    
    private void OnResourcesScanned(IReadOnlyList<CollectableResource> resources)
    {
        if (_unitCreator.FreeCount == 0)
            return;
        
        List<CollectableResource> freeResources = FilterResources(resources);
        int maxJobs = Math.Min(_unitCreator.FreeCount, freeResources.Count);
        
        for (int i = 0; i < maxJobs; i++)
        {
            CollectableResource resource = freeResources[i];
            
            if (_unitCreator.TryGetFreeObject(out Unit unit) == false)
                continue;
            
            _unitCreator.TryReserve(unit);
            _resourceCreator.TryReserve(resource);
            
            resource.SetStateReserved();
            unit.CollectResource(resource);
        }
    }
    
    private List<CollectableResource> FilterResources(IReadOnlyList<CollectableResource> resources)
    {
        var filtered = new List<CollectableResource>();
        
        foreach (CollectableResource resource in resources)
            if (_resourceCreator.IsFree(resource))
                filtered.Add(resource);
        
        return filtered;
    }
    
    private void OnUnitCollectedResource(Unit unit, CollectableResource resource)
    {
        resource.SetStateCollected();
        unit.MoveTo(gameObject.transform);
    }
    
    private void OnUnitStoredResource(Unit unit, CollectableResource resource)
    {
        _unitCreator.TryFree(unit);
        _resourceCreator.TryFree(resource);
        _stateMachine.PerformCurrentState(state =>
        {
            (state as ResourceBaseCollectState)?.NotifyResourceCollected();
        });
    }
    
    private void OnUnitCreatedResourceBase(Unit unit)
    {
        _flagCreator.Deactivate();
        UnitCreatedResourceBase?.Invoke(this, unit, _flagCreator.Position);
    }
}
