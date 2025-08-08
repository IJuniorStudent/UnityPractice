using UnityEngine;

public class RaySensor : CollisionSensor
{
    public RaySensor(Transform selfTransform, float distance, LayerMask layerMask) : base(selfTransform, distance, layerMask) { }
    
    public override bool Probe(Vector3 offset, Vector3 direction)
    {
        IsDetected = Physics.Raycast(SelfTransform.position + offset, direction, out RaycastHit hit, Distance, CastMask);
        
        if (IsDetected == false)
            return false;
        
        Point = hit.point;
        Normal = hit.normal;
        
        return true;
    }
}
