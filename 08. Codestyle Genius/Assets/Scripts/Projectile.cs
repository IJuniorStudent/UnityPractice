using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _maxFlyDistance = 10.0f;
    
    private bool _isMaxDistanceReached = false;
    private float _maxFlyDistanceSquared;
    private Vector3 _sourcePosition;
    private Rigidbody _rigidbody;
    
    public event Action<Projectile> Collided;
    public event Action<Projectile> MaxDistanceReached;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _maxFlyDistanceSquared = _maxFlyDistance * _maxFlyDistance;
    }
    
    private void Update()
    {
        if (_isMaxDistanceReached)
            return;
        
        if ((transform.position - _sourcePosition).sqrMagnitude >= _maxFlyDistanceSquared)
        {
            _isMaxDistanceReached = true;
            MaxDistanceReached?.Invoke(this);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Collided?.Invoke(this);
    }
    
    public void Initialize(Vector3 startPosition, Vector3 target, Vector3 velocity)
    {
        transform.position = startPosition;
        transform.forward = (target - startPosition).normalized;
        _rigidbody.velocity = velocity;
        _isMaxDistanceReached = false;
        _sourcePosition = startPosition;
    }
}
