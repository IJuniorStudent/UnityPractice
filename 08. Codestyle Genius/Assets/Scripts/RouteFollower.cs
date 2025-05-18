using UnityEngine;

public class RouteFollower : MonoBehaviour
{
    private const float DistanceCheckThreshold = 0.01f;
    
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private Transform _pointsContainer;
    
    private bool _isMoving;
    private int _targetPointIndex;
    private Vector3 _targetPoint;
    private Vector3[] _routePoints;
    
    private void Start()
    {
        InitializeRoute();
        StartMove();
    }
    
    private void Update()
    {
        if (_isMoving == false)
            return;
        
        if (IsTargetReached())
            UpdateMoveTarget();
        
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _moveSpeed * Time.deltaTime);
    }

    private void InitializeRoute()
    {
        int pointsCount = _pointsContainer.childCount;
        _routePoints = new Vector3[pointsCount];
        
        for (int i = 0; i < pointsCount; i++)
            _routePoints[i] = _pointsContainer.GetChild(i).transform.position;
        
        _targetPoint = _routePoints[_targetPointIndex];
    }
    
    public void StartMove()
    {
        if (_routePoints.Length == 0)
            return;
        
        LookAtCurrentTarget();
        _isMoving = true;
    }
    
    private bool IsTargetReached()
    {
        return (_targetPoint - transform.position).sqrMagnitude <= DistanceCheckThreshold;
    }
    
    private void UpdateMoveTarget()
    {
        _targetPointIndex = (_targetPointIndex + 1) % _routePoints.Length;
        _targetPoint = _routePoints[_targetPointIndex];
        
        LookAtCurrentTarget();
    }
    
    private void LookAtCurrentTarget()
    {
        transform.forward = _targetPoint - transform.position;
    }
}
