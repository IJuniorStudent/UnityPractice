using DG.Tweening;
using UnityEngine;

public class Scaler : TransformMutator
{
    protected override void BeginMutate()
    {
        SelfTransform
            .DOScale(SelfTransform.localScale + Vector3.one * Speed, 1.0f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
}
