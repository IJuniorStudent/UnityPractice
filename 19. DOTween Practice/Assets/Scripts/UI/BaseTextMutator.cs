using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTextMutator : MonoBehaviour
{
    [SerializeField] protected Text Caption;
    [SerializeField] protected string NewCaption;
    [SerializeField] protected float Speed = 1.0f;
    [SerializeField] protected Ease EaseType = Ease.Linear;
    
    public abstract Tweener InitializeMutation();
}
