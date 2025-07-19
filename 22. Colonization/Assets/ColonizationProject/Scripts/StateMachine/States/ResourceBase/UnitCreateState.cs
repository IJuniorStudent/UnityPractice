public class UnitCreateState : ResourceBaseCollectState
{
    public UnitCreateState(ResourceBase owner, ResourceStorage storage, int requiredResourcesCount) : base(owner, storage, requiredResourcesCount) {}
    
    protected override void ExecuteAction()
    {
        Owner.TryCreateNewUnit(MinRequiredResourcesCount);
    }
}
