using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlinker : BaseMutator
{
    [SerializeField] private Color _targetColor;
    
    private Color _sourceColor;
    private Material _material;
    
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _sourceColor = _material.color;
    }
    
    protected override void BeginMutate()
    {
        DOTween.Sequence()
            .Append(_material.DOColor(_targetColor, Speed))
            .Append(_material.DOColor(_sourceColor, Speed))
            .SetLoops(-1, LoopType.Restart)
            .Play();
    }
}
