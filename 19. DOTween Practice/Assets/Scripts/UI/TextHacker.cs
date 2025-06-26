using DG.Tweening;
using UnityEngine;

public class TextHacker : BaseTextMutator
{
    [SerializeField] private ScrambleMode _scrambleMode;
    
    public override Tweener InitializeMutation()
    {
        return Caption.DOText(NewCaption, Speed, true, _scrambleMode).SetEase(EaseType);
    }
}
