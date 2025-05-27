using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    private const float FallingVelocityThreshold = 0.01f;
    
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _jumpForce = 400.0f;
    
    private Rigidbody2D _rigidBody;
    private bool _isJumping;
    private bool _isFalling;
    
    public event Action Jumped;
    public event Action FallStarted;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _groundDetector.Stepped += OnGroundStepped;
    }

    private void OnDisable()
    {
        _groundDetector.Stepped -= OnGroundStepped;
    }

    private void Update()
    {
        if (_isJumping == false || _isFalling || _rigidBody.velocity.y > -FallingVelocityThreshold)
            return;
        
        _isFalling = true;
        FallStarted?.Invoke();
    }
    
    public void Jump()
    {
        if (_isJumping)
            return;
        
        _isJumping = true;
        Jumped?.Invoke();
        
        _rigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    private void OnGroundStepped()
    {
        _isJumping = false;
        _isFalling = false;
    }
}
