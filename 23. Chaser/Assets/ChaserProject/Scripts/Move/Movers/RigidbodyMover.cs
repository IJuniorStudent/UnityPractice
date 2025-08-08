using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMover : BaseMover
{
    [SerializeField] private float _maxSlopeAngleDegrees = 55.0f;
    [SerializeField] private Vector3 _gravityForce = Physics.gravity;
    [SerializeField] private float _gravityScale = 1.0f;
    
    private MoveStrategySelector _moveStrategySelector;
    private GravityEmulator _gravity;
    private GroundMoveSensor _sensor;
    
    private Rigidbody _rigidbody;
    private Vector3 _lastSurfaceNormal;
    private Vector3 _freeFallAcceleration;
    
    public Rigidbody Rigidbody => _rigidbody;
    
    protected override void Awake()
    {
        base.Awake();
        
        _rigidbody = GetComponent<Rigidbody>();
        _gravity = new GravityEmulator(_gravityForce, _gravityScale);
        _sensor = new GroundMoveSensor(_rigidbody.transform, _gravity, GroundDetectLayerMask, MinVerticalClimbHeight, _maxSlopeAngleDegrees);
        
        var strategyFactory = new GroundMoveStrategyFactory();
        
        _moveStrategySelector = new MoveStrategySelector(_sensor, strategyFactory.Create(this));
        _lastSurfaceNormal = _gravity.Up;
    }
    
    public override void MoveTo(Transform target)
    {
        base.MoveTo(target);
        _rigidbody.transform.forward = GetTargetHorizontalDirection();
    }
    
    protected override void Move()
    {
        if (_moveStrategySelector.TrySelect(out StrategyBase strategy) == false)
            return;
        
        strategy.Activate();
        
        _rigidbody.transform.forward = strategy.CalculateViewDirection(Target);
        
        Vector3 moveDirection = strategy.CalculateMoveDirection(_sensor, _lastSurfaceNormal, out Vector3 surfaceNormal);
        Vector3 moveVelocity = moveDirection * Speed;
        
        if (strategy.UseGravity)
        {
            _freeFallAcceleration += _gravity.FixedForceDelta;
            moveVelocity = _gravity.Affect(_rigidbody.velocity, _freeFallAcceleration, moveVelocity);
        }
        else
        {
            _freeFallAcceleration = Vector3.zero;
        }
        
        _rigidbody.velocity = moveVelocity;
        _lastSurfaceNormal = surfaceNormal;
    }
    
    public override void Stop()
    {
        base.Stop();
        _rigidbody.velocity = Vector3.zero;
    }
}
