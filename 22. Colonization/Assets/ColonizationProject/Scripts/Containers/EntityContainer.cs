using System.Collections.Generic;
using UnityEngine;

public class EntityContainer<T> : MonoBehaviour where T : MonoBehaviour
{
    private HashSet<T> _freeObjects = new();
    private HashSet<T> _reservedObjects = new();
    
    public int FreeCount => _freeObjects.Count;
    public int TotalCount => _freeObjects.Count + _reservedObjects.Count;
    
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
    
    public void Register(T instance)
    {
        Unregister(instance);
        _freeObjects.Add(instance);
    }
    
    public void Unregister(T instance)
    {
        if (_freeObjects.Remove(instance) == false)
            _reservedObjects.Remove(instance);
    }
    
    protected bool IsFree(T instance)
    {
        return _freeObjects.Contains(instance);
    }
    
    protected bool Has(T instance)
    {
        return _freeObjects.Contains(instance) || _reservedObjects.Contains(instance);
    }
}
