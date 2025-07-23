using System;
using System.Collections;
using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    private const float MaxInitialRotationAngle = 360.0f;
    
    [SerializeField] private MultiBoxArea _spawnArea;
    [SerializeField] private Unit _prefab;
    
    public event Action<Unit> Created;
    
    public void Create(int count)
    {
        StartCoroutine(CreateUnits(count));
    }
    
    public void Relocate(Unit unit)
    {
        Transform unitTransform = unit.transform;
        unitTransform.position = _spawnArea.GetRandomPoint();
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
        Created?.Invoke(newUnit);
    }
}
