using UnityEngine;

public abstract class CollisionSensor
{
    protected Transform SelfTransform;
    protected float Distance;
    protected LayerMask CastMask;
    
    public bool IsDetected { get; protected set; }
    public Vector3 Point { get; protected set; }
    public Vector3 Normal { get; protected set; }
    
    protected CollisionSensor(Transform selfTransform, float distance, LayerMask layerMask)
    {
        SelfTransform = selfTransform;
        Distance = distance;
        CastMask = layerMask;
    }
    
    public void Reset()
    {
        IsDetected = false;
    }
    
    public abstract bool Probe(Vector3 offset, Vector3 direction);
}
