using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : MonoBehaviour
{
    [SerializeField] private MultiBoxArea _multiBoxArea;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private int _initialUnitsCount = 3;
    [SerializeField] private int _scanInterval = 3;
    
    private HashSet<Unit> _freeUnits = new();
    private HashSet<Unit> _busyUnits = new();
    
    private WaitForSeconds _scanDelay;
    
    private void Awake()
    {
        _scanDelay = new WaitForSeconds(_scanInterval);
    }
    
    private void Start()
    {
        StartCoroutine(SpawnUnits(_initialUnitsCount));
        StartCoroutine(ScanResources());
    }
    
    private void OnEnable()
    {
        _resourceScanner.ResourcesCollected += OnResourcesScanned;
    }
    
    private void OnDisable()
    {
        _resourceScanner.ResourcesCollected -= OnResourcesScanned;
    }
    
    private IEnumerator SpawnUnits(int unitsCount)
    {
        for (int i = 0; i < unitsCount; i++)
        {
            SpawnUnit();
            yield return null;
        }
    }
    
    private void SpawnUnit()
    {
        Vector3 position = _multiBoxArea.GetRandomPoint();
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.value * 360.0f, 0);
        Unit unit = _unitSpawner.Spawn(position, rotation);

        unit.ResourceCollected += OnUnitCollectedResource;
        unit.ResourceStored += OnUnitStoredResource;
        
        _freeUnits.Add(unit);
    }
    
    private IEnumerator ScanResources()
    {
        while (enabled)
        {
            yield return _scanDelay;
            _resourceScanner.Scan();
        }
    }
    
    private bool TryGetFreeUnit(out Unit freeUnit)
    {
        foreach (Unit unit in _freeUnits)
        {
            freeUnit = unit;
            return true;
        }
        
        freeUnit = null;
        return false;
    }
    
    private void SetUnitBusy(Unit unit)
    {
        _freeUnits.Remove(unit);
        _busyUnits.Add(unit);
    }
    
    private void SetUnitFree(Unit unit)
    {
        _busyUnits.Remove(unit);
        _freeUnits.Add(unit);
    }
    
    private void OnResourcesScanned(List<CollectableResource> resources)
    {
        int maxJobs = Math.Min(_freeUnits.Count, resources.Count);
        
        for (int i = 0; i < maxJobs; i++)
        {
            CollectableResource resource = resources[i];
            
            if (TryGetFreeUnit(out Unit unit) == false)
                continue;
            
            unit.CollectResource(resource);
            
            SetUnitBusy(unit);
        }
    }
    
    private void OnUnitCollectedResource(Unit unit)
    {
        unit.MoveTo(gameObject.transform);
    }
    
    private void OnUnitStoredResource(Unit unit)
    {
        SetUnitFree(unit);
    }
}
