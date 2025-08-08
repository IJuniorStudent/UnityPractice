using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMover : BaseMover
{
    private CharacterController _controller;
    private float _minSlopeCosine;
    
    protected override void Awake()
    {
        base.Awake();
        _controller = GetComponent<CharacterController>();
        _minSlopeCosine = Mathf.Cos(_controller.slopeLimit * Mathf.Deg2Rad);
    }
    
    protected override void Move()
    {
        Vector3 direction = GetTargetHorizontalDirection();
        Vector3 velocity = Speed * Time.fixedDeltaTime * direction;
        
        _controller.transform.forward = direction;
        
        TryClimb();
        _controller.Move(velocity + Physics.gravity * Time.fixedDeltaTime);
    }
    
    private void TryClimb()
    {
        Vector3 selfPosition = gameObject.transform.position;
        Vector3 direction = gameObject.transform.forward;
        Vector3 castPosition = selfPosition + Vector3.up * MinClimbHeight;
        float castDistance = 0.55f;
        
        if (Physics.Raycast(castPosition, direction, out RaycastHit hit, castDistance, GroundDetectLayerMask) == false)
            return;
        
        if (IsSlopeAngleExceeded(hit.normal) == false)
            return;
        
        float sphereCastHeight = 1.0f;
        float sphereCastRadius = 0.25f;
        float sphereCastForwardOffset = 0.35f;
        Vector3 sphereCastPosition = selfPosition + Vector3.up * sphereCastHeight + direction * sphereCastForwardOffset;
        
        if (Physics.SphereCast(sphereCastPosition, sphereCastRadius, Vector3.down, out RaycastHit sphereHit, sphereCastHeight, GroundDetectLayerMask) == false)
            return;
        
        float heightDelta = sphereHit.point.y - selfPosition.y;
        
        if (heightDelta < MinClimbHeight || heightDelta > MaxClimbHeight)
            return;
        
        if (IsSlopeAngleExceeded(sphereHit.normal))
            return;
        
        _controller.enabled = false;
        _controller.transform.position = sphereHit.point;
        _controller.enabled = true;
    }
    
    private bool IsSlopeAngleExceeded(Vector3 normal)
    {
        return Vector3.Dot(normal, Vector3.up) < _minSlopeCosine;
    }
}
