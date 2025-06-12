using System;
using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> : BaseSpawner where T: LifeLimitedEntity
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 50;
    
    private ObjectPool<T> _pool;
    
    public event Action<T> ObjectLifetimeExpired;
    
    private void Awake()
    {
        _pool = new ObjectPool<T>
        (
            createFunc: CreateObject,
            actionOnGet: ReuseObject,
            actionOnRelease: DisableObject,
            actionOnDestroy: DestroyObject,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }
    
    public T Spawn(Vector3 position, Quaternion rotation)
    {
        T spawnedObject = _pool.Get();
        
        spawnedObject.Reset();
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        
        InvokeObjectSpawned();
        
        return spawnedObject;
    }
    
    public void Despawn(T spawnedObject)
    {
        _pool.Release(spawnedObject);
    }
    
    private T CreateObject()
    {
        T spawnedObject = Instantiate(_prefab);
        
        spawnedObject.LifetimeExpired += OnLifetimeExpired;
        InvokeObjectCreated();
        
        return spawnedObject;
    }
    
    private void ReuseObject(T disabledObject)
    {
        disabledObject.gameObject.SetActive(true);
        InvokeObjectActivated();
    }
    
    private void DisableObject(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
        InvokeObjectDeactivated();
    }
    
    private void DestroyObject(T spawnedObject)
    {
        spawnedObject.LifetimeExpired -= OnLifetimeExpired;
        Destroy(spawnedObject.gameObject);
    }
    
    private void OnLifetimeExpired(LifeLimitedEntity entity)
    {
        ObjectLifetimeExpired?.Invoke(entity as T);
    }
}
