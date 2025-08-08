using UnityEngine;

public class StrategyMove : ControllableStrategy
{
    private const float Epsilon = 0.0001f;
    private const int MaxGroundCollisions = 5;
    
    private RaycastHit[] _raycastCache;
    private float _maxClimbHeight;
    
    public StrategyMove(Rigidbody rigidbody, float maxClimbHeight) : base(rigidbody, false)
    {
        _raycastCache = new RaycastHit[MaxGroundCollisions];
        _maxClimbHeight = maxClimbHeight;
    }
    
    public override bool Evaluate(GroundMoveSensor groundSensor)
    {
        if (groundSensor.IsGrounded == false || groundSensor.HasGroundAhead == false)
            return false;
        
        if (groundSensor.IsSlopeAngleExceeded(groundSensor.SelfPosition.Normal))
            return false;
        
        float heightDelta = groundSensor.StepHeightDelta;
        
        if (Mathf.Abs(heightDelta) <= Epsilon)
            return true;
        
        bool isPredictSlopeAngleExceeded = groundSensor.IsSlopeAngleExceeded(groundSensor.NextPosition.Normal);
        
        if (heightDelta > 0)
        {
            if (isPredictSlopeAngleExceeded)
                return true;
            
            if (heightDelta > _maxClimbHeight)
                return true;
            
            if (groundSensor.HasObstacleInFront == false)
                return true;
            
            if (groundSensor.IsSlopeAngleExceeded(groundSensor.Front.Normal) == false)
                return true;
        }
        
        return isPredictSlopeAngleExceeded == false;
    }
    
    public override Vector3 CalculateMoveDirection(GroundMoveSensor groundSensor, Vector3 lastSurfaceNormal, out Vector3 surfaceNormal)
    {
        int hitCount = groundSensor.FindGroundCollisions(SelfTransform.position, _raycastCache);
        surfaceNormal = CalculateResultNormal(groundSensor, hitCount);
        
        return Vector3.ProjectOnPlane(SelfTransform.forward, surfaceNormal);
    }
    
    private Vector3 CalculateResultNormal(GroundMoveSensor groundSensor, int hitCount)
    {
        if (hitCount == 0)
            return groundSensor.Gravity.Up;
        
        Vector3 normal = Vector3.zero;
        
        for (int i = 0; i < hitCount; i++)
            normal += _raycastCache[i].normal;
        
        if (hitCount > 1 && groundSensor.NextPosition.IsDetected)
        {
            normal += groundSensor.NextPosition.Normal;
            hitCount++;
        }
        
        normal /= hitCount;
        normal.Normalize();
        
        return normal;
    }
}
