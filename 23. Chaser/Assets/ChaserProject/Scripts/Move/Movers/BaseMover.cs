using System;
using UnityEngine;

public abstract class BaseMover : MonoBehaviour
{
    [SerializeField] protected float MinVerticalClimbHeight = 0.15f;
    [SerializeField] protected float MaxVerticalClimbHeight = 0.5f;
    [SerializeField] protected float Speed = 5.0f;
    [SerializeField] protected float ReachDistanceThreshold = 0.05f;
    [SerializeField] protected LayerMask GroundDetectLayerMask;
    
    protected float SquaredThreshold;
    protected Transform Target;
    
    public float MinClimbHeight => MinVerticalClimbHeight;
    public float MaxClimbHeight => MaxVerticalClimbHeight;
    public bool IsMoving { get; protected set; }
    
    public event Action TargetReached;
    
    protected virtual void Awake()
    {
        SquaredThreshold = ReachDistanceThreshold * ReachDistanceThreshold;
    }
    
    private void FixedUpdate()
    {
        if (IsMoving == false)
            return;
        
        if (IsTargetReached())
        {
            Stop();
            return;
        }
        
        Move();
    }
    
    public virtual void MoveTo(Transform target)
    {
        Target = target;
        IsMoving = true;
    }
    
    public virtual void Stop()
    {
        IsMoving = false;
        TargetReached?.Invoke();
    }
    
    protected Vector3 GetTargetHorizontalDirection()
    {
        Vector3 selfPosition = gameObject.transform.position;
        Vector3 targetPosition = Target.position;
        
        selfPosition.y = 0;
        targetPosition.y = 0;
        
        return (targetPosition - selfPosition).normalized;
    }
    
    protected abstract void Move();
    
    private bool IsTargetReached()
    {
        Vector3 selfPosition = gameObject.transform.position;
        Vector3 targetPosition = Target.position;
        
        Vector2 selfPositionFlat = new Vector2(selfPosition.x, selfPosition.z);
        Vector2 targetPositionFlat = new Vector2(targetPosition.x, targetPosition.z);
        
        if ((targetPositionFlat - selfPositionFlat).sqrMagnitude > SquaredThreshold)
            return false;
        
        if (Mathf.Abs(targetPosition.y - selfPosition.y) > MaxVerticalClimbHeight)
            return false;
        
        return true;
    }
}
