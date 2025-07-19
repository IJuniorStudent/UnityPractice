using System;
using System.Collections.Generic;

public abstract class BaseStateFactory<TOwner>
{
    protected TOwner Owner;
    
    protected BaseStateFactory(TOwner owner)
    {
        Owner = owner;
    }
    
    public abstract Dictionary<Type, BaseState<TOwner>> CreateStates();
}