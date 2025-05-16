using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float DistanceCheckThreshold = 0.1f;
    
    [SerializeField] private float _moveSpeed = 5.0f;
    
    private bool _isMoving = false;
    private Vector3 _targetPosition;
    private Vector3 _direction;
    
    public event Action<Enemy> DestinationReached;
    
    private void Update()
    {
        if (_isMoving == false)
            return;
        
        if (IsDestinationReached())
        {
            _isMoving = false;
            DestinationReached?.Invoke(this);
            return;
        }
        
        transform.Translate(_moveSpeed * Time.deltaTime * _direction, Space.World);
    }
    
    public void StartMove(Vector3 destination)
    {
        _targetPosition = destination;
        _direction = (_targetPosition - transform.position).normalized;
        
        _isMoving = true;
    }
    
    private bool IsDestinationReached()
    {
        return (_targetPosition - transform.position).sqrMagnitude <= DistanceCheckThreshold;
    }
}
