using UnityEngine;

public class MoveState : BaseState
{
    private const float TargetDistanceReachThreshold = 0.01f;
    
    private bool _isMoving;
    private Vector3 _direction;
    
    public MoveState(Unit owner) : base(owner) { }
    
    public override void Enter()
    {
        Transform ownerTransform = Owner.gameObject.transform;
        
        _direction = (Owner.MoveTarget.position - ownerTransform.position).normalized;
        ownerTransform.forward = _direction;
        
        _isMoving = true;
    }
    
    public override void Update()
    {
        if (_isMoving == false)
            return;
        
        Transform ownerTransform = Owner.gameObject.transform;
        
        if ((Owner.MoveTarget.position - ownerTransform.position).sqrMagnitude < TargetDistanceReachThreshold)
        {
            _isMoving = false;
            return;
        }
        
        ownerTransform.Translate(Time.deltaTime * Owner.MoveSpeed * _direction, Space.World);
    }
    
    public override void Exit()
    {
        _isMoving = false;
    }
}
