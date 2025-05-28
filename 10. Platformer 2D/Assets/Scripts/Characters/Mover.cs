using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    
    private Rigidbody2D _rigidBody;
    
    private bool _isMoving;
    private float _moveDirection;
    
    public bool IsMoving => _isMoving;
    public float VerticalSpeed => _rigidBody.velocity.y;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (_isMoving)
            _rigidBody.velocity = new Vector2(_moveDirection * _speed, _rigidBody.velocity.y);
    }
    
    public void StartMove(float axisDelta)
    {
        _isMoving = true;
        _moveDirection = axisDelta;
    }
    
    public void StopMove()
    {
        _isMoving = false;
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
    }
}
