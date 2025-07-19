public class ResourceBaseCreateState : ResourceBaseCollectState
{
    public ResourceBaseCreateState(ResourceBase owner, ResourceStorage storage, int requiredResourcesCount) : base(owner, storage, requiredResourcesCount) {}
    
    protected override void ExecuteAction()
    {
        Owner.TryCreateNewBase(MinRequiredResourcesCount);
    }
}
