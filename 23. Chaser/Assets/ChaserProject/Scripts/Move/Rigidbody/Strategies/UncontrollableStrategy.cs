using UnityEngine;

public abstract class UncontrollableStrategy : StrategyBase
{
    protected UncontrollableStrategy(Rigidbody rigidbody, bool useGravity) : base(rigidbody, useGravity) { }
    
    public override Vector3 CalculateViewDirection(Transform target)
    {
        return SelfTransform.forward;
    }
}
