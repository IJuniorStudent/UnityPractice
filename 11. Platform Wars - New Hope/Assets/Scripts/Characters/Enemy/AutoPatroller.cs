using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class AutoPatroller : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _startMoveProbability = 0.2f;
    [SerializeField] private float _startMoveTaskInterval = 1.0f;
    [SerializeField] private float _patrolRadius = 5.0f;
    [SerializeField] private float _patrolPositionOffset = 0.0f;
    
    private BotMover _botMover;
    private Coroutine _patrolCoroutine;
    private WaitForSeconds _patrolWait;
    
    private float[] _patrolPoints;
    private int _targetPatrolPointIndex;
    
    private bool _isMoving;
    
    private void Awake()
    {
        _botMover = GetComponent<BotMover>();
        _patrolWait = new WaitForSeconds(_startMoveTaskInterval);
        
        float positionX = gameObject.transform.position.x;
        
        _patrolPoints = new float[]
        {
            positionX + _patrolPositionOffset - _patrolRadius,
            positionX + _patrolPositionOffset + _patrolRadius
        };
        
        _targetPatrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
    }
    
    private void OnEnable()
    {
        _botMover.MoveFinished += OnPatrolTargetReached;
    }
    
    private void OnDisable()
    {
        _botMover.MoveFinished -= OnPatrolTargetReached;
    }
    
    private void OnDrawGizmosSelected()
    {
        DrawPatrolArea();
    }
    
    public void StartTask()
    {
        _patrolCoroutine = StartCoroutine(PatrolTask());
    }
    
    public void StopTask()
    {
        if (_isMoving)
            _botMover.StopMove();
        
        TerminateCoroutine(ref _patrolCoroutine);
    }
    
    private IEnumerator PatrolTask()
    {
        while (enabled)
        {
            yield return _patrolWait;
            
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= _startMoveProbability)
            {
                _isMoving = true;
                _botMover.MoveTo(_patrolPoints[_targetPatrolPointIndex]);
                _targetPatrolPointIndex = (_targetPatrolPointIndex + 1) % _patrolPoints.Length;
                
                while (_isMoving)
                    yield return null;
            }
        }
    }
    
    private void TerminateCoroutine(ref Coroutine coroutine)
    {
        if (coroutine == null)
            return;
        
        StopCoroutine(coroutine);
        coroutine = null;
    }
    
    private void OnPatrolTargetReached()
    {
        _isMoving = false;
    }

    private void DrawPatrolArea()
    {
        Vector3 ownerPosition = gameObject.transform.position;
        
        float startPoint;
        float endPoint;
        
        if (Application.isPlaying)
        {
            startPoint = _patrolPoints[0];
            endPoint = _patrolPoints[1];
        }
        else
        {
            startPoint = ownerPosition.x + _patrolPositionOffset - _patrolRadius;
            endPoint = ownerPosition.x + _patrolPositionOffset + _patrolRadius;
        }
        
        Vector3 drawPosition = new Vector3(startPoint + (endPoint - startPoint) / 2.0f, ownerPosition.y, ownerPosition.z);
        drawPosition.x += _patrolPositionOffset;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(drawPosition, new Vector3(_patrolRadius * 2.0f, 1.0f, 0.0f));
    }
}
