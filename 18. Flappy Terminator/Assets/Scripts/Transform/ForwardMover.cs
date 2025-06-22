using UnityEngine;

public class ForwardMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private Transform _lookDirection;
    
    private bool _isMoving = true;
    
    private void Update()
    {
        if (_isMoving)
            gameObject.transform.Translate(_speed * Time.deltaTime * _lookDirection.right, Space.World);
    }
    
    public void StartMove()
    {
        _isMoving = true;
    }
    
    public void StopMove()
    {
        _isMoving = false;
    }
}
