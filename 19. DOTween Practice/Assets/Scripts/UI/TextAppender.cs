using DG.Tweening;

public class TextAppender : BaseTextMutator
{
    public override Tweener InitializeMutation()
    {
        return Caption.DOText(NewCaption, Speed).SetEase(EaseType).SetRelative();
    }
}
