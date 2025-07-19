using System;
using System.Collections;
using UnityEngine;

public class UnitCreator : GenericCreator<Unit>
{
    [SerializeField] private Unit _prefab;
    
    public event Action<Unit> Created;
    
    public void Create(int count)
    {
        StartCoroutine(CreateUnits(count));
    }
    
    public void Link(Unit unit)
    {
        Register(unit);
    }
    
    public void Unlink(Unit unit)
    {
        Unregister(unit);
    }
    
    public void Relocate(Unit unit)
    {
        Transform unitTransform = unit.transform;
        unitTransform.position = SpawnArea.GetRandomPoint();
        unitTransform.rotation = Quaternion.Euler(0, UnityEngine.Random.value * MaxInitialRotationAngle, 0);
    }
    
    private IEnumerator CreateUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateUnit();
            yield return null;
        }
    }
    
    private void CreateUnit()
    {
        Unit newUnit = Instantiate(_prefab);
        
        Register(newUnit);
        
        Created?.Invoke(newUnit);
    }
}
