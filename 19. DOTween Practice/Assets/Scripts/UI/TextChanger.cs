using DG.Tweening;

public class TextChanger : BaseTextMutator
{
    public override Tweener InitializeMutation()
    {
        return Caption.DOText(NewCaption, Speed).SetEase(EaseType);
    }
}
