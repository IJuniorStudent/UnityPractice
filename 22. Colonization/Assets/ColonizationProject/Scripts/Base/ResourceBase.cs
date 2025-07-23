using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : MonoBehaviour, ICollectorTarget, IRaycastTarget
{
    private const int MinUnitsToCreateBase = 2;
    
    [SerializeField] private FlagCreator _flagCreator;
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private UnitContainer _unitContainer;
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private int _scanInterval = 3;
    [SerializeField] private int _unitCreateCost = 3;
    [SerializeField] private int _baseCreateCost = 5;
    
    private ResourceContainer _resourceContainer;
    private WaitForSeconds _scanDelay;
    private bool _isBaseCreateWait;
    
    public event Action<bool> SelectStateChanged;
    public event Action<ResourceBase, Unit, Vector3> UnitCreatedResourceBase;
    public event Action<int> UnitCountChanged;
    public event Action<CollectableResource> ResourceStored;
    
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
    
    public void Initialize(ResourceContainer resourceContainer, ResourceBaseFoundation flagPrefab)
    {
        _resourceContainer = resourceContainer;
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
        
        if (_unitContainer.TotalCount < MinUnitsToCreateBase)
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
        _unitContainer.Register(unit);
        _unitCreator.Relocate(unit);
        
        UnitCountChanged?.Invoke(_unitContainer.TotalCount);
        
        unit.ResourceDelivered += OnUnitDeliveredResource;
        unit.ResourceBaseCreated += OnUnitCreatedResourceBase;
        
        unit.SetHomeBase(this);
    }
    
    public void Unlink(Unit unit)
    {
        unit.ResourceDelivered -= OnUnitDeliveredResource;
        unit.ResourceBaseCreated -= OnUnitCreatedResourceBase;
        
        _unitContainer.Unregister(unit);
        UnitCountChanged?.Invoke(_unitContainer.TotalCount);
    }
    
    private void TryCreateNewUnit()
    {
        if (_resourceStorage.TrySpend(_unitCreateCost))
            _unitCreator.Create(1);
    }
    
    private void TryCreateNewBase()
    {
        if (_unitContainer.FreeCount == 0)
            return;
        
        if (_unitContainer.TryGetFreeObject(out Unit unit) == false)
            return;
        
        if (_unitContainer.TryReserve(unit) == false)
            return;
        
        if (_resourceStorage.TrySpend(_baseCreateCost) == false)
        {
            _unitContainer.TryFree(unit);
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
    
    private void OnResourcesScanned(List<CollectableResource> resources)
    {
        _resourceContainer.TryRegister(resources, this);
        
        if (_resourceContainer.TryGetSorted(this, _unitContainer.FreeCount, out List<CollectableResource> sortedResources) == false)
            return;
        
        foreach (CollectableResource resource in sortedResources)
        {
            if (_unitContainer.TryGetFreeObject(out Unit unit) == false)
                continue;
            
            _unitContainer.TryReserve(unit);
            _resourceContainer.TryReserve(resource);
            
            resource.SetStateReserved();
            unit.CollectResource(resource);
        }
    }
    
    private void OnUnitDeliveredResource(Unit unit)
    {
        if (unit.TryDetachResource(out CollectableResource resource) == false)
            return;
        
        _unitContainer.TryFree(unit);
        _resourceStorage.Store(resource.Value);
        _resourceContainer.TryFree(resource);
        
        resource.ResetState();
        ResourceStored?.Invoke(resource);
        
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
