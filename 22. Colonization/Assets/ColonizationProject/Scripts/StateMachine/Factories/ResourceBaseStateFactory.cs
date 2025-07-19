using System;
using System.Collections.Generic;

public class ResourceBaseStateFactory : BaseStateFactory<ResourceBase>
{
    private const int RequiredResourcesToCreateUnit = 3;
    private const int RequiredResourcesToCreateBase = 5;
    
    private ResourceStorage _storage;
    
    public ResourceBaseStateFactory(ResourceBase owner, ResourceStorage storage) : base(owner)
    {
        _storage = storage;
    }
    
    public override Dictionary<Type, BaseState<ResourceBase>> CreateStates()
    {
        return new Dictionary<Type, BaseState<ResourceBase>>
        {
            [typeof(UnitCreateState)] = new UnitCreateState(Owner, _storage, RequiredResourcesToCreateUnit),
            [typeof(ResourceBaseCreateState)] = new ResourceBaseCreateState(Owner, _storage, RequiredResourcesToCreateBase)
        };
    }
}
