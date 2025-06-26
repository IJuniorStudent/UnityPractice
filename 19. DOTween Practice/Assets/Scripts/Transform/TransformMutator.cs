using UnityEngine;

[RequireComponent(typeof(Transform))]
public abstract class TransformMutator : BaseMutator
{
    protected Transform SelfTransform;
    
    private void Awake()
    {
        SelfTransform = GetComponent<Transform>();
    }
}
