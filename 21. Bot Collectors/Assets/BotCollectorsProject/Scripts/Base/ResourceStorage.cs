using System;

public class ResourceStorage : CollectorTarget
{
    private int _resourcesCount;
    
    public event Action<int> AmountChanged;
    
    public void Store(int count)
    {
        _resourcesCount += count;
        AmountChanged?.Invoke(_resourcesCount);
    }
}
