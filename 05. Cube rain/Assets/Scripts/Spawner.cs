using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxArea))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _cubesCapacity = 10;
    [SerializeField] private int _cubesMaxCount = 20;
    
    private BoxArea _boxArea;
    private ObjectPool<Cube> _pool;
    
    private void Awake()
    {
        _boxArea = GetComponent<BoxArea>();
        
        _pool = new ObjectPool<Cube>(
            createFunc: CreateCube,
            actionOnGet: ReuseCube,
            actionOnRelease: DisableCube,
            actionOnDestroy: DestroyCube,
            collectionCheck: true,
            defaultCapacity: _cubesCapacity,
            maxSize: _cubesMaxCount
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes(_cubesCapacity));
    }
    
    private Cube CreateCube()
    {
        Cube cube = Instantiate(_prefab);
        RandomizeStartPosition(cube.gameObject.transform);
        
        cube.LifetimeExpired += OnLifetimeExpired;
        
        return cube;
    }
    
    private void DestroyCube(Cube cube)
    {
        cube.LifetimeExpired -= OnLifetimeExpired;
        
        Destroy(cube.gameObject);
    }

    private void ReuseCube(Cube detector)
    {
        RandomizeStartPosition(detector.gameObject.transform);
        
        detector.gameObject.SetActive(true);
    }

    private void DisableCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void RandomizeStartPosition(Transform targetTransform)
    {
        targetTransform.position = _boxArea.GetRandomPoint();
        targetTransform.rotation = Quaternion.Euler(0, Random.Range(0, 360), Random.Range(0, 360));
    }
    
    private void OnLifetimeExpired(Cube cube)
    {
        _pool.Release(cube);
        _pool.Get();
    }
    
    private IEnumerator SpawnCubes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _pool.Get();
            yield return null;
        }
    }
}
