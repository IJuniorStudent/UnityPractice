using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400.0f;
    
    private Rigidbody2D _rigidBody;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    public void Jump()
    {
        _rigidBody.AddForce(new Vector2(0, _jumpForce));
    }
}
