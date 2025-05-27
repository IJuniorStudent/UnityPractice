using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class BotMover : MonoBehaviour
{
    private const float CheckDistanceThreshold = 0.01f;
    
    private Mover _mover;
    private Transform _parentTransform;
    private Vector3 _targetPosition;
    
    public event Action TargetReached;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _parentTransform = gameObject.transform;
    }
    
    private void Update()
    {
        if (_mover.IsMoving == false)
            return;

        if (IsTargetReached())
        {
            _mover.StopMove();
            TargetReached?.Invoke();
        }
    }
    
    public void MoveTo(float x, float y)
    {
        _targetPosition = new Vector3(x, y, _parentTransform.position.z);
        
        float moveDistance = x - _parentTransform.position.x;
        _mover.StartMove(moveDistance / Mathf.Abs(moveDistance));
    }
    
    private bool IsTargetReached()
    {
        return (_targetPosition - _parentTransform.position).sqrMagnitude <= CheckDistanceThreshold;
    }
}
