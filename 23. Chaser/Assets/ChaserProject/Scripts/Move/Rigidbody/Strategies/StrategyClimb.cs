using UnityEngine;

public class StrategyClimb : ControllableStrategy
{
    private Vector3 _targetPosition;
    private float _minClimbHeight;
    private float _maxClimbHeight;
    
    public StrategyClimb(Rigidbody rigidbody, float minClimbHeight, float maxClimbHeight) : base(rigidbody, false)
    {
        _minClimbHeight = minClimbHeight;
        _maxClimbHeight = maxClimbHeight;
    }
    
    public override bool Evaluate(GroundMoveSensor groundSensor)
    {
        if (groundSensor.IsGrounded == false || groundSensor.HasGroundAhead == false || groundSensor.HasObstacleInFront == false)
            return false;
        
        if (groundSensor.IsSlopeAngleExceeded(groundSensor.SelfPosition.Normal))
            return false;
        
        float heightDelta = groundSensor.StepHeightDelta;
        
        if (heightDelta < _minClimbHeight || heightDelta > _maxClimbHeight)
            return false;
        
        if (groundSensor.IsSlopeAngleExceeded(groundSensor.NextPosition.Normal))
            return false;
        
        if (groundSensor.IsSlopeAngleExceeded(groundSensor.Front.Normal) == false)
            return false;
        
        _targetPosition = groundSensor.NextPosition.Point;
        return true;
    }
    
    public override Vector3 CalculateMoveDirection(GroundMoveSensor groundSensor, Vector3 lastSurfaceNormal, out Vector3 surfaceNormal)
    {
        surfaceNormal = groundSensor.NextPosition.Normal;
        return Vector3.ProjectOnPlane(SelfTransform.forward, surfaceNormal);
    }
    
    public override void Activate()
    {
        RigidBody.position = _targetPosition;
    }
}
