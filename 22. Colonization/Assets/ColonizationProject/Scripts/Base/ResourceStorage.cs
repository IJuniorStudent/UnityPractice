using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _resourcesCount;
    
    public event Action<int> AmountChanged;
    
    public void Store(int count)
    {
        _resourcesCount += count;
        AmountChanged?.Invoke(_resourcesCount);
    }
    
    public bool TrySpend(int count)
    {
        if (_resourcesCount < count)
            return false;
        
        _resourcesCount -= count;
        AmountChanged?.Invoke(_resourcesCount);
        
        return true;
    }
}
