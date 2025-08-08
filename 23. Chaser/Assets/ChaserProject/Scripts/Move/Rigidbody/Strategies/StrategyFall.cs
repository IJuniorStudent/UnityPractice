using UnityEngine;

public class StrategyFall : UncontrollableStrategy
{
    public StrategyFall(Rigidbody rigidbody) : base(rigidbody, true) { }
    
    public override bool Evaluate(GroundMoveSensor groundSensor)
    {
        return groundSensor.IsGrounded == false;
    }
    
    public override Vector3 CalculateMoveDirection(GroundMoveSensor groundSensor, Vector3 lastSurfaceNormal, out Vector3 surfaceNormal)
    {
        surfaceNormal = groundSensor.Gravity.Up;
        return SelfTransform.forward;
    }
}
