using System;
using UnityEngine;

[Serializable]
public struct Route
{
    [SerializeField] private Vector3 _start;
    [SerializeField] private Vector3 _destination;
    
    public Vector3 Start => _start;
    public Vector3 Destination => _destination;

    public Quaternion CalculateVerticalRotation()
    {
        Vector3 direction2d = new Vector3(_destination.x, 0, _destination.z) - new Vector3(_start.x, 0, _start.z);
        float angle = Mathf.Atan2(direction2d.x, direction2d.z) * Mathf.Rad2Deg;
        
        return Quaternion.Euler(0, angle, 0);
    }
    
    public void DebugDraw()
    {
        Gizmos.color = new Color(0.7f, 0.7f, 0);
        Gizmos.DrawLine(_start, _destination);

        Gizmos.color = new Color(0, 0.7f, 0);
        Gizmos.DrawSphere(_start, 0.1f);

        Gizmos.color = new Color(0.7f, 0, 0);
        Gizmos.DrawSphere(_destination, 0.1f);
    }
}
