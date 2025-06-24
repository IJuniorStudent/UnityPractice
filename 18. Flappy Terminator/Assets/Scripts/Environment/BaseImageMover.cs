using UnityEngine;

public abstract class BaseImageMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    
    private float _startPositionX;
    private bool _isMoving;
    
    protected virtual void Awake()
    {
        _startPositionX = gameObject.transform.position.x;
        _isMoving = true;
    }
    
    private void Update()
    {
        if (_isMoving)
            gameObject.transform.Translate(_speed * Time.deltaTime * Vector3.left);
    }
    
    public void StartMove()
    {
        _isMoving = true;
    }
    
    public void StopMove()
    {
        _isMoving = false;
    }
    
    public abstract void ResetState();
    
    protected void ResetPosition()
    {
        Vector3 position = gameObject.transform.position;
        position.x = _startPositionX;
        gameObject.transform.position = position;
    }
}
