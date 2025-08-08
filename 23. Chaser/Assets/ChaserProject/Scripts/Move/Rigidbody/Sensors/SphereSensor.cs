using UnityEngine;

public class SphereSensor : CollisionSensor
{
    private float _radius;
    
    public SphereSensor(Transform selfTransform, float distance, LayerMask layerMask, float radius) : base(selfTransform, distance, layerMask)
    {
        _radius = radius;
    }
    
    public override bool Probe(Vector3 offset, Vector3 direction)
    {
        IsDetected = Physics.SphereCast(SelfTransform.position + offset, _radius, direction, out RaycastHit hit, Distance - _radius, CastMask);
        
        if (IsDetected == false)
            return false;
        
        Point = hit.point;
        Normal = hit.normal;
        
        return true;
    }
}
