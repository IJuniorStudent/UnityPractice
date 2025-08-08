using UnityEngine;

public class StrategyTrampoline : ControllableStrategy
{
    public StrategyTrampoline(Rigidbody rigidbody) : base(rigidbody, false) { }
    
    public override bool Evaluate(GroundMoveSensor groundSensor)
    {
        if (groundSensor.IsGrounded == false)
            return false;
        
        if (groundSensor.IsSlopeAngleExceeded(groundSensor.SelfPosition.Normal))
            return false;
        
        if (groundSensor.HasGroundAhead == false)
            return true;
        
        return groundSensor.StepHeightDelta <= 0 && groundSensor.IsSlopeAngleExceeded(groundSensor.NextPosition.Normal);
    }
    
    public override Vector3 CalculateMoveDirection(GroundMoveSensor groundSensor, Vector3 lastSurfaceNormal, out Vector3 surfaceNormal)
    {
        surfaceNormal = lastSurfaceNormal;
        return Vector3.ProjectOnPlane(SelfTransform.forward, surfaceNormal);
    }
}
