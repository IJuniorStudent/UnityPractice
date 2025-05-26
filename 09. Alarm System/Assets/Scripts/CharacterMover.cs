using System;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private const float DistanceCheckThreshold = 0.01f;
    
    [SerializeField] private float _speed = 5.0f;
    
    private Transform _parentTransform;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    
    public event Action TargetReached;

    private void Awake()
    {
        _parentTransform = gameObject.transform;
    }
    
    private void Update()
    {
        if (_isMoving == false)
            return;
        
        if (IsTargetReached())
        {
            _isMoving = false;
            TargetReached?.Invoke();
            return;
        }
        
        _parentTransform.Translate(Time.deltaTime * _speed * _parentTransform.forward, Space.World);
    }
    
    public void MoveTo(Vector3 targetPosition)
    {
        _parentTransform.forward = (targetPosition - _parentTransform.position).normalized;
        _targetPosition = targetPosition;
        _isMoving = true;
    }
    
    private bool IsTargetReached()
    {
        return (_targetPosition - _parentTransform.position).sqrMagnitude <= DistanceCheckThreshold;
    }
}
