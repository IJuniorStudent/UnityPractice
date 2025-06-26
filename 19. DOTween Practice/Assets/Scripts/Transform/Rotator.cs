using DG.Tweening;
using UnityEngine;

public class Rotator : TransformMutator
{
    [SerializeField] private RotateMode _rotateMode;
    
    protected override void BeginMutate()
    {
        SelfTransform
            .DORotate(new Vector3(0, Speed, 0), 1.0f, _rotateMode)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
}
