using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class BotMover : MonoBehaviour
{
    private const float CheckDistanceThreshold = 0.01f;
    
    [SerializeField] private MonsterAnimator _monsterAnimator;
    
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
        if (_mover.IsMoving == false || IsTargetReached() == false)
            return;
        
        _mover.StopMove();
        _monsterAnimator.StopMove();
        
        TargetReached?.Invoke();
    }
    
    public void MoveTo(float x, float y)
    {
        _targetPosition = new Vector3(x, y, _parentTransform.position.z);
        
        float moveDistance = x - _parentTransform.position.x;
        _mover.StartMove(moveDistance / Mathf.Abs(moveDistance));
        
        bool isMoveForward = moveDistance >= 0.0f;
        _monsterAnimator.StartMove(isMoveForward);
    }
    
    private bool IsTargetReached()
    {
        return (_targetPosition - _parentTransform.position).sqrMagnitude <= CheckDistanceThreshold;
    }
}
