using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float DistanceCheckThreshold = 1.0f;
    
    [SerializeField] private float _moveSpeed = 5.0f;
    
    private bool _isMoving = false;
    private Transform _target;
    
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
        
        Vector3 direction = (_target.position - transform.position).normalized;
        
        transform.rotation = transform.position.VerticalLookAt(_target.position);
        transform.Translate(_moveSpeed * Time.deltaTime * direction, Space.World);
    }
    
    public void StartMove(Transform target)
    {
        _target = target;
        _isMoving = true;
    }
    
    private bool IsDestinationReached()
    {
        return (_target.position - transform.position).sqrMagnitude <= DistanceCheckThreshold;
    }
}
