using DG.Tweening;
using UnityEngine;

public class TextSequentialMutator : BaseMutator
{
    [SerializeField] private LoopType _loopType;
    [SerializeField] private BaseTextMutator[] _mutators;
    
    protected override void BeginMutate()
    {
        Sequence sequence = DOTween.Sequence();
        
        foreach (var mutator in _mutators)
            sequence.Append(mutator.InitializeMutation());
        
        sequence
            .SetLoops(-1, _loopType)
            .Play();
    }
}
