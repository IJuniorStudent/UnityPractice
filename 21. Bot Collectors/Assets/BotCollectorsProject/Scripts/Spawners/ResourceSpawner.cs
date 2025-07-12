public class ResourceSpawner : GenericSpawner<CollectableResource>
{
    protected override void OnObjectCreate(CollectableResource createdObject)
    {
        createdObject.Stored += Despawn;
    }
    
    protected override void OnObjectDestroy(CollectableResource objectToDestroy)
    {
        objectToDestroy.Stored -= Despawn;
    }
}
