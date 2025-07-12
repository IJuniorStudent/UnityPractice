using System.Collections.Generic;
using UnityEngine;

public class GenericCreator<T> : MonoBehaviour where T : MonoBehaviour
{
    protected const float MaxInitialRotationAngle = 360.0f;
    
    [SerializeField] protected MultiBoxArea SpawnArea;
    
    private HashSet<T> _freeObjects = new();
    private HashSet<T> _reservedObjects = new();
    
    public int FreeCount => _freeObjects.Count;
    
    public bool IsFree(T instance)
    {
        return _freeObjects.Contains(instance);
    }
    
    public bool TryGetFreeObject(out T freeObject)
    {
        foreach (var instance in _freeObjects)
        {
            freeObject = instance;
            return true;
        }
        
        freeObject = null;
        return false;
    }
    
    public bool TryReserve(T instance)
    {
        if (_freeObjects.Contains(instance) == false)
            return false;
        
        _freeObjects.Remove(instance);
        _reservedObjects.Add(instance);
        
        return true;
    }
    
    public virtual bool TryFree(T instance)
    {
        if (_reservedObjects.Contains(instance) == false)
            return false;
        
        _reservedObjects.Remove(instance);
        _freeObjects.Add(instance);
        
        return true;
    }
    
    protected void Register(T instance)
    {
        _freeObjects.Add(instance);
    }
    
    protected void Unregister(T instance)
    {
        if (_freeObjects.Remove(instance) == false)
            _reservedObjects.Remove(instance);
    }
}
