using UnityEngine;

public abstract class ControllableStrategy : StrategyBase
{
    protected ControllableStrategy(Rigidbody rigidbody, bool useGravity) : base(rigidbody, useGravity) { }
    
    public override Vector3 CalculateViewDirection(Transform target)
    {
        Vector3 selfPosition = SelfTransform.position;
        Vector3 targetPosition = target.position;
        
        selfPosition.y = 0;
        targetPosition.y = 0;
        
        return (targetPosition - selfPosition).normalized;
    }
}
