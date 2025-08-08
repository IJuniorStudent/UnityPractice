using UnityEngine;

public abstract class StrategyBase
{
    protected Rigidbody RigidBody;
    protected Transform SelfTransform;
    
    public bool UseGravity { get; }
    
    protected StrategyBase(Rigidbody rigidBody, bool useGravity)
    {
        RigidBody = rigidBody;
        SelfTransform = rigidBody.transform;
        UseGravity = useGravity;
    }
    
    public abstract Vector3 CalculateViewDirection(Transform target);
    public abstract bool Evaluate(GroundMoveSensor groundSensor);
    public abstract Vector3 CalculateMoveDirection(GroundMoveSensor groundSensor, Vector3 lastSurfaceNormal, out Vector3 surfaceNormal);
    public virtual void Activate() { }
}
