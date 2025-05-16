using System;
using System.Collections.Generic;
using UnityEngine;

public class RouteTraveler : MonoBehaviour
{
    private const float DistanceCheckThreshold = 0.1f;
    
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private List<Vector3> _routePoints;
    
    private int _targetPointIndex = 0;
    private bool _isMoving = false;
    private Vector3 _direction;

    private void Start()
    {
        StartMove();
    }
    
    private void Update()
    {
        if (_isMoving == false)
            return;
        
        if (IsReachedTarget())
        {
            _targetPointIndex = (_targetPointIndex + 1) % _routePoints.Count;
            UpdateMoveTarget();
        }
        
        transform.Translate(_moveSpeed * Time.deltaTime * _direction, Space.World);
    }
    
    private void OnDrawGizmos()
    {
        DebugDrawRoute();
    }
    
    private void StartMove()
    {
        UpdateMoveTarget();
        _isMoving = true;
    }
    
    private bool IsReachedTarget()
    {
        return (gameObject.transform.position - _routePoints[_targetPointIndex]).sqrMagnitude <= DistanceCheckThreshold;
    }
    
    private void UpdateMoveTarget()
    {
        Transform selfTransform = gameObject.transform;
        
        Vector3 position = selfTransform.position;
        Vector3 target = _routePoints[_targetPointIndex];
        
        _direction = (target - position).normalized;
        
        selfTransform.rotation = position.VerticalLookAt(target);
    }
    
    [ContextMenu("Add current point to route")]
    private void AddCurrentPoint()
    {
        _routePoints.Add(transform.position);
    }
    
    private void DebugDrawRoute()
    {
        Color startPointColor = new Color(0, 0.7f, 0);
        Color midPointColor = new Color(0.7f, 0, 0);
        int pointsCount = _routePoints.Count;
        
        for (int i = 0; i < pointsCount; i++)
        {
            Color pointColor = i == 0 ? startPointColor : midPointColor;
            Vector3 startPoint = _routePoints[i];
            Vector3 nextPoint = _routePoints[(i + 1) % pointsCount];
            
            DrawRouteChunk(startPoint, nextPoint, pointColor);
        }
    }
    
    private void DrawRouteChunk(Vector3 startPosition, Vector3 endPosition, Color pointColor)
    {
        Gizmos.color = new Color(0.7f, 0.7f, 0);
        Gizmos.DrawLine(startPosition, endPosition);
        
        Gizmos.color = pointColor;
        Gizmos.DrawSphere(startPosition, 0.1f);
    }
}
