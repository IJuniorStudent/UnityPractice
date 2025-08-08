using UnityEngine;
using System.Collections.Generic;

public class GroundMoveStrategyFactory : MoveStrategyFactory
{
    public override List<StrategyBase> Create(RigidbodyMover ownerMover)
    {
        Rigidbody parentRigidbody = ownerMover.Rigidbody;
        
        return new List<StrategyBase>
        {
            new StrategySlide(parentRigidbody),
            new StrategyClimb(parentRigidbody, ownerMover.MinClimbHeight, ownerMover.MaxClimbHeight),
            new StrategyTrampoline(parentRigidbody),
            new StrategyMove(parentRigidbody, ownerMover.MaxClimbHeight),
            new StrategyFall(parentRigidbody)
        };
    }
}
