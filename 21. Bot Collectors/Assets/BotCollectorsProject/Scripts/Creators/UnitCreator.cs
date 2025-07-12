using System;
using System.Collections;
using UnityEngine;

public class UnitCreator : GenericCreator<Unit>
{
    [SerializeField] private int _initialCount = 3;
    [SerializeField] private Unit _prefab;
    
    public event Action<Unit> Created;
    
    private void Start()
    {
        StartCoroutine(CreateUnits());
    }
    
    private IEnumerator CreateUnits()
    {
        for (int i = 0; i < _initialCount; i++)
        {
            Create();
            yield return null;
        }
    }
    
    private void Create()
    {
        Vector3 position = SpawnArea.GetRandomPoint();
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.value * MaxInitialRotationAngle, 0);
        
        Unit newUnit = Instantiate(_prefab, position, rotation);
        Register(newUnit);
        
        Created?.Invoke(newUnit);
    }
}
