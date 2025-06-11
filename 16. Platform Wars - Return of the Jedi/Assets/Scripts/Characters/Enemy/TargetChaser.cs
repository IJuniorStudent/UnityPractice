using System;
using System.Collections;
using UnityEngine;

public class TargetChaser : MonoBehaviour
{
    private const float BorderDistance = 0.5f;
    
    [SerializeField] private float _moveUpdateInterval = 1.0f;
    
    private float _minX;
    private float _maxX;
    private bool _isChasing;
    private float _squaredDistance;

    private Coroutine _moveUpdateRequest;
    private WaitForSeconds _updateWait;
    
    private Transform _target;

    public event Action<Vector3> MoveTargetRequested;
    public event Action Finished;
    
    private void Awake()
    {
        _updateWait = new WaitForSeconds(_moveUpdateInterval);
    }
    
    private void FixedUpdate()
    {
        if (_isChasing == false)
            return;
        
        if (IsTargetReached() == false && IsReachedBorder() == false)
            return;
        
        _isChasing = false;
        Finished?.Invoke();
    }
    
    public void Initialize(float minX, float maxX, float targetReachDistance)
    {
        _minX = minX;
        _maxX = maxX;
        _squaredDistance = targetReachDistance * targetReachDistance;
    }
    
    public void StartTask(Transform target)
    {
        _target = target;
        _isChasing = true;
        
        _moveUpdateRequest = StartCoroutine(RequestMove());
    }
    
    public void StopTask()
    {
        _isChasing = false;
        
        if (_moveUpdateRequest != null)
        {
            StopCoroutine(_moveUpdateRequest);
            _moveUpdateRequest = null;
        }
    }
    
    private bool IsTargetReached()
    {
        return (_target.position - gameObject.transform.position).sqrMagnitude <= _squaredDistance;
    }
    
    private bool IsReachedBorder()
    {
        float positionX = gameObject.transform.position.x;
        
        return Mathf.Abs(positionX - _minX) < BorderDistance || Mathf.Abs(positionX - _maxX) < BorderDistance;
    }
    
    private IEnumerator RequestMove()
    {
        while (_isChasing)
        {
            MoveTargetRequested?.Invoke(_target.position);
            yield return _updateWait;
        }
    }
}
