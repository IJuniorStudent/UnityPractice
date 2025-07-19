public abstract class ResourceBaseCollectState : BaseState<ResourceBase>
{
    protected int MinRequiredResourcesCount;
    
    private ResourceStorage _storage;
    private int _initialResourcesCount;

    protected ResourceBaseCollectState(ResourceBase owner, ResourceStorage storage, int requiredResourcesCount) : base(owner)
    {
        _storage = storage;
        _initialResourcesCount = storage.Count;
        MinRequiredResourcesCount = requiredResourcesCount;
    }
    
    public void NotifyResourceCollected()
    {
        int collectedResources = _storage.Count - _initialResourcesCount;
        
        if (collectedResources >= MinRequiredResourcesCount)
            ExecuteAction();
    }
    
    protected abstract void ExecuteAction();
}
