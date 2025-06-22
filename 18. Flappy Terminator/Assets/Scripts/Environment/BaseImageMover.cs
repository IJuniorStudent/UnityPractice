using UnityEngine;

[RequireComponent(typeof(EventGate))]
public abstract class BaseImageMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    
    private EventGate _eventGate;
    private float _startPositionX;
    private bool _isMoving;
    
    protected virtual void Awake()
    {
        _eventGate = GetComponent<EventGate>();
        _startPositionX = gameObject.transform.position.x;
        _isMoving = true;
    }
    
    private void OnEnable()
    {
        _eventGate.PlayerDied += OnPlayerDied;
        _eventGate.Restarted += OnRestarted;
    }
    
    private void OnDisable()
    {
        _eventGate.PlayerDied -= OnPlayerDied;
        _eventGate.Restarted -= OnRestarted;
    }
    
    private void Update()
    {
        if (_isMoving)
            gameObject.transform.Translate(_speed * Time.deltaTime * Vector3.left);
    }
    
    protected void ResetPosition()
    {
        Vector3 position = gameObject.transform.position;
        position.x = _startPositionX;
        gameObject.transform.position = position;
    }
    
    public abstract void ResetState();
    
    private void OnPlayerDied()
    {
        _isMoving = false;
    }
    
    private void OnRestarted()
    {
        _isMoving = true;
    }
}
