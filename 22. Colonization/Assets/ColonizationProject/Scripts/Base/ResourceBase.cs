using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : MonoBehaviour, ICollectorTarget, IRaycastTarget
{
    private const int MinUnitsToCreateBase = 2;
    
    [SerializeField] private FlagCreator _flagCreator;
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private int _scanInterval = 3;
    [SerializeField] private int _unitCreateCost = 3;
    [SerializeField] private int _baseCreateCost = 5;
    
    private ResourceCreator _resourceCreator;
    private WaitForSeconds _scanDelay;
    private bool _isBaseCreateWait;
    
    public event Action<bool> SelectStateChanged;
    public event Action<ResourceBase, Unit, Vector3> UnitCreatedResourceBase;
    public event Action<int> UnitCountChanged;
    
    private void Awake()
    {
        _scanDelay = new WaitForSeconds(_scanInterval);
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
        if (_flagCreator.IsActive)
        {
            _flagCreator.UpdatePosition(position);
            return;
        }
        
        if (_unitCreator.TotalCount < MinUnitsToCreateBase)
            return;
        
        _flagCreator.Activate(position);
        _isBaseCreateWait = true;
    }
    
    public void CreateInitialUnits(int count)
    {
        _unitCreator.Create(count);
    }
    
    public void Link(Unit unit)
    {
        _unitCreator.Link(unit);
        _unitCreator.Relocate(unit);
        
        UnitCountChanged?.Invoke(_unitCreator.TotalCount);
        
        unit.ResourceDelivered += OnUnitDeliveredResource;
        unit.ResourceBaseCreated += OnUnitCreatedResourceBase;
        
        unit.SetHomeBase(this);
    }
    
    public void Unlink(Unit unit)
    {
        unit.ResourceDelivered -= OnUnitDeliveredResource;
        unit.ResourceBaseCreated -= OnUnitCreatedResourceBase;
        
        _unitCreator.Unlink(unit);
        UnitCountChanged?.Invoke(_unitCreator.TotalCount);
    }
    
    private void TryCreateNewUnit()
    {
        if (_resourceStorage.TrySpend(_unitCreateCost))
            _unitCreator.Create(1);
    }
    
    private void TryCreateNewBase()
    {
        if (_unitCreator.FreeCount == 0)
            return;
        
        if (_unitCreator.TryGetFreeObject(out Unit unit) == false)
            return;
        
        if (_unitCreator.TryReserve(unit) == false)
            return;
        
        if (_resourceStorage.TrySpend(_baseCreateCost) == false)
        {
            _unitCreator.TryFree(unit);
            return;
        }
        
        _flagCreator.SetWorker(unit);
        _isBaseCreateWait = false;
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
        
        List<CollectableResource> freeResources = _resourceCreator.FilterFreeSortedResources(resources, gameObject.transform.position);
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
    
    private void OnUnitDeliveredResource(Unit unit)
    {
        if (unit.TryDetachResource(out CollectableResource resource) == false)
            return;
        
        _resourceStorage.Store(resource.Value);
        _resourceCreator.TryFree(resource);
        _unitCreator.TryFree(unit);
        
        SelectNextJob();
    }
    
    private void OnUnitCreatedResourceBase(Unit unit)
    {
        _flagCreator.Deactivate();
        UnitCreatedResourceBase?.Invoke(this, unit, _flagCreator.Position);
    }
    
    private void SelectNextJob()
    {
        if (_flagCreator.IsActive && _isBaseCreateWait)
            TryCreateNewBase();
        else
            TryCreateNewUnit();
    }
}
