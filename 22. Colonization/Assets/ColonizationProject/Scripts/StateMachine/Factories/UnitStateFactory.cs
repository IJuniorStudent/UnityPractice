using System;
using System.Collections.Generic;

public class UnitStateFactory: BaseStateFactory<Unit>
{
    public UnitStateFactory(Unit owner) : base(owner) { }
    
    public override Dictionary<Type, BaseState<Unit>> CreateStates()
    {
        return new Dictionary<Type, BaseState<Unit>>
        {
            [typeof(UnitIdleState)] = new UnitIdleState(Owner),
            [typeof(UnitMoveState)] = new UnitMoveState(Owner)
        };
    }
}
