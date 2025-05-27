using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    
    private Rigidbody2D _rigidBody;
    
    private bool _isMoving;
    private float _moveDirection;
    
    public bool IsMoving => _isMoving;
    
    public event Action MoveForwardStarted;
    public event Action MoveBackwardStarted;
    public event Action MoveStopped;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (_isMoving)
            _rigidBody.velocity = new Vector2(_moveDirection * _speed, _rigidBody.velocity.y);
    }
    
    public void StartMove(float axisDelta)
    {
        _isMoving = true;
        _moveDirection = axisDelta;
        
        Action moveEvent = _moveDirection > 0.0f ? MoveForwardStarted : MoveBackwardStarted;
        moveEvent?.Invoke();
    }
    
    public void StopMove()
    {
        _isMoving = false;
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        
        MoveStopped?.Invoke();
    }
}
