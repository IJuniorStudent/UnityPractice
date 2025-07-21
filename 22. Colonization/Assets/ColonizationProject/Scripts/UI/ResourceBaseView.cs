using UnityEngine;
using UnityEngine.UI;

public class ResourceBaseView : MonoBehaviour
{
    [SerializeField] private ResourceBase _parentBase;
    [SerializeField] private ParticleSystem _selectionEffect;
    [SerializeField] private Text _unitsCountText;
    
    private void OnEnable()
    {
        _parentBase.SelectStateChanged += OnSelectStateChanged;
        _parentBase.UnitCountChanged += OnUnitCountChanged;
    }
    
    private void OnDisable()
    {
        _parentBase.SelectStateChanged -= OnSelectStateChanged;
        _parentBase.UnitCountChanged -= OnUnitCountChanged;
    }
    
    private void OnSelectStateChanged(bool isSelected)
    {
        _selectionEffect.gameObject.SetActive(isSelected);
    }
    
    private void OnUnitCountChanged(int unitsCount)
    {
        _unitsCountText.text = unitsCount.ToString();
    }
}
