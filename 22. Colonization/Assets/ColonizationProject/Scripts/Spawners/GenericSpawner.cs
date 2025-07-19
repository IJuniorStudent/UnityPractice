using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private T _prefab;
    
    private ObjectPool<T> _pool;
    
    public event Action<T> ObjectCreated;
    public event Action<T> ObjectDestroyed;
    
    private void Awake()
    {
        _pool = new ObjectPool<T>(
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
        
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        
        return spawnedObject;
    }
    
    public void Despawn(T objectToDespawn)
    {
        _pool.Release(objectToDespawn);
    }
    
    private T CreateObject()
    {
        T createdObject = Instantiate(_prefab);
        ObjectCreated?.Invoke(createdObject);
        
        return createdObject;
    }
    
    private void ReuseObject(T objectToReuse)
    {
        objectToReuse.gameObject.SetActive(true);
    }
    
    private void DisableObject(T objectToDisable)
    {
        objectToDisable.gameObject.SetActive(false);
    }
    
    private void DestroyObject(T objectToDestroy)
    {
        ObjectDestroyed?.Invoke(objectToDestroy);
        Destroy(objectToDestroy.gameObject);
    }
}
