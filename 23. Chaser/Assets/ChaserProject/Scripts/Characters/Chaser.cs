using System.Collections;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private BaseMover _mover;
    [SerializeField] private float _stopDistance = 2.0f;
    [SerializeField] private float _targetUpdateInterval = 0.2f;
    
    private float _stopDistanceSquared;
    private WaitForSeconds _waitTargetUpdate;
    
    private void Awake()
    {
        _stopDistanceSquared = _stopDistance * _stopDistance;
        _waitTargetUpdate = new WaitForSeconds(_targetUpdateInterval);
    }
    
    private void OnEnable()
    {
        StartCoroutine(Chase());
    }
    
    private IEnumerator Chase()
    {
        while (enabled)
        {
            yield return _waitTargetUpdate;
            
            bool isReached = IsTargetReached();
            
            if (isReached == false && _mover.IsMoving == false)
                _mover.MoveTo(_target);
            
            if (isReached && _mover.IsMoving)
                _mover.Stop();
        }
    }
    
    private bool IsTargetReached()
    {
        return (_target.position - gameObject.transform.position).sqrMagnitude <= _stopDistanceSquared;
    }
}
