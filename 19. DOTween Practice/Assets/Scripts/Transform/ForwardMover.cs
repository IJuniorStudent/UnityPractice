using DG.Tweening;
using UnityEngine;

public class ForwardMover : TransformMutator
{
    protected override void BeginMutate()
    {
        SelfTransform
            .DOMove(SelfTransform.position + SelfTransform.forward * Speed, 1.0f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
}
