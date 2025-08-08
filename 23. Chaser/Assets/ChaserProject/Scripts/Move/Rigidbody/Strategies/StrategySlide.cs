using UnityEngine;

public class StrategySlide : ControllableStrategy
{
    public StrategySlide(Rigidbody rigidbody) : base(rigidbody, true) { }
    
    public override bool Evaluate(GroundMoveSensor groundSensor)
    {
        if (groundSensor.IsGrounded == false)
            return false;
        
        return groundSensor.IsSlopeAngleExceeded(groundSensor.SelfPosition.Normal);
    }
    
    public override Vector3 CalculateMoveDirection(GroundMoveSensor groundSensor, Vector3 lastSurfaceNormal, out Vector3 surfaceNormal)
    {
        surfaceNormal = groundSensor.Gravity.Up;
        return SelfTransform.forward;
    }
}
