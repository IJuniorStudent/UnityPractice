using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private T _prefab;
    
    private ObjectPool<T> _pool;
    private HashSet<T> _activeObjects;
    
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
        
        _activeObjects = new HashSet<T>();
    }
    
    public void ReleaseActive()
    {
        foreach (T activeObject in _activeObjects)
            _pool.Release(activeObject);
        
        _activeObjects.Clear();
    }
    
    public T Spawn(Vector3 position, Quaternion rotation)
    {
        T spawnedObject = _pool.Get();
        
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;
        
        _activeObjects.Add(spawnedObject);
        
        return spawnedObject;
    }
    
    protected void Despawn(T objectToDespawn)
    {
        _activeObjects.Remove(objectToDespawn);
        _pool.Release(objectToDespawn);
    }

    protected virtual void OnObjectCreate(T createdObject) { }
    protected virtual void OnObjectDestroy(T objectToDestroy) { }
    
    private T CreateObject()
    {
        T createdObject = Instantiate(_prefab);
        OnObjectCreate(createdObject);
        
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
        OnObjectDestroy(objectToDestroy);
        Destroy(objectToDestroy.gameObject);
    }
}
