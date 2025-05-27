using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(BotMover))]
public class AutoPatroller : MonoBehaviour
{
    [SerializeField] private float _maxPatrolRadius = 5.0f;
    [SerializeField] private float _patrolTaskInterval = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _startMoveProbability = 0.2f;
    
    private BotMover _botMover;
    
    private float _minPositionX;
    private float _maxPositionX;
    private WaitForSeconds _waitBeforeNextTask;
    private bool _hasMoveTask;
    
    private void Awake()
    {
        _botMover = GetComponent<BotMover>();
        
        _minPositionX = gameObject.transform.position.x - _maxPatrolRadius;
        _maxPositionX = gameObject.transform.position.x + _maxPatrolRadius;
        
        _waitBeforeNextTask = new WaitForSeconds(_patrolTaskInterval);
        
        StartCoroutine(Walk());
    }

    private void OnEnable()
    {
        _botMover.TargetReached += OnTargetReached;
    }
    
    private void OnDisable()
    {
        _botMover.TargetReached -= OnTargetReached;
    }
    
    private IEnumerator Walk()
    {
        while (enabled)
        {
            yield return _waitBeforeNextTask;
            TryStartMove();
        }
    }
    
    private void TryStartMove()
    {
        if (_hasMoveTask || UnityEngine.Random.value > _startMoveProbability)
            return;
        
        float targetX = UnityEngine.Random.Range(_minPositionX, _maxPositionX);
        _botMover.MoveTo(targetX, gameObject.transform.position.y);
        _hasMoveTask = true;
    }
    
    private void OnTargetReached()
    {
        _hasMoveTask = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(_maxPatrolRadius * 2.0f, 1.0f, 0.0f));
    }
}
