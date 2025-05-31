using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rotator))]
public class BotMover : MonoBehaviour
{
    private const float DistanceThreshold = 0.1f;
    
    private Mover _mover;
    private Rotator _rotator;
    
    private bool _isMoving;
    private float _targetX;
    
    public event Action MoveStarted;
    public event Action MoveFinished;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
    }
    
    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;
        
        if (IsTargetReached() == false)
            return;
        
        StopMove();
    }
    
    public void MoveTo(float targetX)
    {
        float distance = targetX - gameObject.transform.position.x;
        bool isLookForward = distance > 0.0f;
        
        _targetX = targetX;
        _isMoving = true;
        
        _rotator.SetDirection(isLookForward);
        _mover.StartMove(distance / Mathf.Abs(distance));
        
        MoveStarted?.Invoke();
    }
    
    public void StopMove()
    {
        _isMoving = false;
        _mover.StopMove();
        
        MoveFinished?.Invoke();
    }
    
    private bool IsTargetReached()
    {
        return Mathf.Abs(_targetX - gameObject.transform.position.x) <= DistanceThreshold;
    }
}
