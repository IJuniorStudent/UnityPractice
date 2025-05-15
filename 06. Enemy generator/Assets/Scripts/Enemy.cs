using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    
    private Vector3 _moveDirection;
    private bool _isMoving;
    
    public void StartMove(Vector3 direction)
    {
        _moveDirection = direction;
        _isMoving = true;
    }
    
    private void Update()
    {
        if (_isMoving)
            transform.Translate(_moveSpeed * Time.deltaTime * _moveDirection, Space.World);
    }
}
