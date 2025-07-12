using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : MonoBehaviour
{
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private ResourceCreator _resourceCreator;
    [SerializeField] private int _scanInterval = 3;
    
    private WaitForSeconds _scanDelay;
    
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
        _unitCreator.Created += OnUnitCreated;
    }
    
    private void OnDisable()
    {
        _resourceScanner.ResourcesCollected -= OnResourcesScanned;
        _unitCreator.Created -= OnUnitCreated;
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
    
    private void OnUnitCreated(Unit unit)
    {
        unit.ResourceCollected += OnUnitCollectedResource;
        unit.ResourceStored += OnUnitStoredResource;
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
    }
}
