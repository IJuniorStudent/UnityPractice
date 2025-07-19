using System;

public class ResourceStorage : CollectorTarget
{
    private int _resourcesCount;
    
    public event Action<int> AmountChanged;
    
    public int Count => _resourcesCount;
    
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
