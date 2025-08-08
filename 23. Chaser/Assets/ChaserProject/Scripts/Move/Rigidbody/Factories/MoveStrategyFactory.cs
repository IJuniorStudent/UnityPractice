using System.Collections.Generic;

public abstract class MoveStrategyFactory
{
    public abstract List<StrategyBase> Create(RigidbodyMover ownerMover);
}
