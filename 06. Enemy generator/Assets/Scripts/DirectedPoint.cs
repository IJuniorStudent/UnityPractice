using System;
using UnityEngine;

[Serializable]
public struct DirectedPoint
{
    [SerializeField] private Vector3 _point;
    [SerializeField, Range(0.0f, 360.0f)] private float _rotateAngle;
    
    public Vector3 Point => _point;
    public Vector3 Direction => CalculateDirection();

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(0, _rotateAngle, 0);
    }
    
    private Vector3 CalculateDirection()
    {
        float halfCircleAngle = 180.0f;
        float radAngle = _rotateAngle / halfCircleAngle * Mathf.PI;
        
        return new Vector3(Mathf.Cos(radAngle), 0, Mathf.Sin(radAngle));
    }
}
