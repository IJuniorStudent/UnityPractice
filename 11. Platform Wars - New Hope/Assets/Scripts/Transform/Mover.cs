using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    
    private Rigidbody2D _rigidbody;
    private float _directionX;
    private bool _isMoving;
    
    public bool IsMoving => _isMoving;
    public float VerticalSpeed => _rigidbody.velocity.y;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (_isMoving)
            _rigidbody.velocity = new Vector2(_directionX * _speed, _rigidbody.velocity.y);
    }
    
    public void StartMove(float directionHorizontal)
    {
        _directionX = directionHorizontal;
        _isMoving = true;
    }
    
    public void StopMove()
    {
        _isMoving = false;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }
}
