using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private MouseRaycaster _raycaster;
    [SerializeField] private ResourceBaseCreator _baseCreator;
    
    private ResourceBase _selectedBase;
    
    private void OnEnable()
    {
        _raycaster.ClickedOnTarget += OnTargetClicked;
    }
    
    private void OnDisable()
    {
        _raycaster.ClickedOnTarget -= OnTargetClicked;
    }
    
    private void OnTargetClicked(RaycastTarget target, Vector3 hitPoint)
    {
        switch (target)
        {
            case ResourceBase resourceBase:
                SelectResourceBase(resourceBase);
                break;
            
            case Ground _:
                TrySetBaseCreatePosition(hitPoint);
                break;
            
            default:
                break;
        }
    }
    
    private void SelectResourceBase(ResourceBase resourceBase)
    {
        if (_selectedBase != resourceBase)
        {
            _baseCreator.SelectBase(resourceBase);
            _selectedBase = resourceBase;
        }
        else
        {
            _baseCreator.Deselect(_selectedBase);
            _selectedBase = null;
        }
    }
    
    private void TrySetBaseCreatePosition(Vector3 position)
    {
        if (_selectedBase == null)
            return;
        
        _selectedBase.TryScheduleBaseCreation(position);
        _selectedBase = null;
    }
}
