using UnityEngine;

public class GroundMoveSensor
{
    private const float ForwardStepDistance = 0.35f;
    
    private Transform _owner;
    private LayerMask _layerMask;
    private float _frontGroundDetectHeight;
    private float _minSlopeCosine;
    
    public GroundMoveSensor(Transform owner, GravityEmulator gravity, LayerMask layerMask, float frontGroundDetectHeight, float slopeAngle)
    {
        _owner = owner;
        _layerMask = layerMask;
        _frontGroundDetectHeight = frontGroundDetectHeight;
        _minSlopeCosine = Mathf.Cos(slopeAngle * Mathf.Deg2Rad);
        Gravity = gravity;
        
        Initialize();
    }
    
    public SphereSensor SelfPosition { get; private set; }
    public SphereSensor NextPosition { get; private set; }
    public RaySensor Front { get; private set; }
    public GravityEmulator Gravity { get; }
    
    public bool IsGrounded => SelfPosition.IsDetected;
    public bool HasGroundAhead => NextPosition.IsDetected;
    public bool HasObstacleInFront => Front.IsDetected;
    public float StepHeightDelta => NextPosition.Point.y - SelfPosition.Point.y;
    
    public void Probe()
    {
        SelfPosition.Reset();
        NextPosition.Reset();
        Front.Reset();
        
        Vector3 forward = _owner.forward;
        
        if (SelfPosition.Probe(Gravity.Up, Gravity.Down))
            if (NextPosition.Probe(Gravity.Up + forward * ForwardStepDistance, Gravity.Down))
                Front.Probe(Gravity.Up * _frontGroundDetectHeight, forward);
    }
    
    public bool IsSlopeAngleExceeded(Vector3 surfaceNormal)
    {
        return Mathf.Abs(Vector3.Dot(surfaceNormal, Gravity.Up)) < _minSlopeCosine;
    }
    
    public int FindGroundCollisions(Vector3 position, RaycastHit[] results)
    {
        float groundOverlapRadius = 0.55f;
        float castStartHeight = 1.0f;
        float castDistanceDelta = 0.05f;
        float castDistance = castStartHeight - groundOverlapRadius + castDistanceDelta;
        
        Vector3 castStartPosition = position + Gravity.Up * castStartHeight;
        
        return Physics.SphereCastNonAlloc(castStartPosition, groundOverlapRadius, Gravity.Down, results, castDistance,_layerMask);
    }
    
    private void Initialize()
    {
        float sphereSensorDistanceDelta = 0.05f;
        
        float selfPositionProbeRadius = 0.45f;
        float nextPositionProbeRadius = 0.25f;
        
        float selfPositionProbeHeight = 1.0f;
        float nextPositionProbeHeight = 2.0f;
        float forwardProbeDistance = 1.0f;
        
        SelfPosition = new SphereSensor(_owner, selfPositionProbeHeight + sphereSensorDistanceDelta, _layerMask, selfPositionProbeRadius);
        NextPosition = new SphereSensor(_owner, nextPositionProbeHeight + sphereSensorDistanceDelta, _layerMask, nextPositionProbeRadius);
        Front = new RaySensor(_owner, forwardProbeDistance, _layerMask);
    }
}
