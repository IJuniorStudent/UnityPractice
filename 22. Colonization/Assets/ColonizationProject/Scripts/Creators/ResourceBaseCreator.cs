using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBaseCreator : MonoBehaviour
{
    [SerializeField] private ResourceCreator _resourceCreator;
    [SerializeField] private ResourceBase _resourceBasePrefab;
    [SerializeField] private ResourceBaseFoundation _flagPrefab;
    [SerializeField] private Transform[] _initialCreatePositions;
    [SerializeField] private int _initialUnitsCount = 3;
    
    private List<ResourceBase> _bases;
    
    private void Awake()
    {
        _bases = new List<ResourceBase>();
    }
    
    private void Start()
    {
        StartCoroutine(CreateInitialBases());
    }
    
    public void SelectBase(ResourceBase selectedResourceBase)
    {
        foreach (ResourceBase resourceBase in _bases)
            resourceBase.SetSelected(resourceBase == selectedResourceBase);
    }
    
    public void Deselect(ResourceBase selectedResourceBase)
    {
        if (_bases.Contains(selectedResourceBase))
            selectedResourceBase.SetSelected(false);
    }
    
    private IEnumerator CreateInitialBases()
    {
        foreach (Transform createPoint in _initialCreatePositions)
        {
            ResourceBase resourceBase = CreateBase(createPoint.position, createPoint.rotation);
            resourceBase.CreateInitialUnits(_initialUnitsCount);
            
            yield return null;
        }
    }
    
    private ResourceBase CreateBase(Vector3 position, Quaternion rotation)
    {
        ResourceBase resourceBase = Instantiate(_resourceBasePrefab, position, rotation);
        resourceBase.Initialize(_resourceCreator, _flagPrefab);
        resourceBase.UnitCreatedResourceBase += OnUnitCreatedResourceBase;
        _bases.Add(resourceBase);
        
        return resourceBase;
    }
    
    private void OnUnitCreatedResourceBase(ResourceBase parentBase, Unit unit, Vector3 position)
    {
        parentBase.Unlink(unit);
        
        ResourceBase newBase = CreateBase(position, Quaternion.identity);
        newBase.Link(unit);
    }
}
