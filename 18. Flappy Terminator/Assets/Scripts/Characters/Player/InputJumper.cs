using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class InputJumper : MonoBehaviour
{
    [SerializeField] private InputReceiver _inputReceiver;
    [SerializeField] private HeightRestrictor _restrictor;
    [SerializeField] private float _verticalVelocity = 10.0f;
    
    private Player _player;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        _inputReceiver.JumpPressed += OnJumpPressed;
    }
    
    private void OnDisable()
    {
        _inputReceiver.JumpPressed -= OnJumpPressed;
    }
    
    private void OnJumpPressed()
    {
        if (_player.IsAlive == false)
            return;
        
        float verticalPosition = gameObject.transform.position.y;
        
        _rigidbody.velocity = _verticalVelocity * _restrictor.GetCeilDistancePercentage(verticalPosition) * Vector2.up;
    }
}
