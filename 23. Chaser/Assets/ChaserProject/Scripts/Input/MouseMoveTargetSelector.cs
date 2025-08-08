using System;
using UnityEngine;

public class MouseMoveTargetSelector : MonoBehaviour
{
    [SerializeField] private MouseRaycaster _mouseRaycaster;
    [SerializeField] private MoveTarget _moveTarget;

    public event Action<Transform> TargetSelected;
    
    private void OnEnable()
    {
        _mouseRaycaster.SurfaceClicked += OnSurfaceClicked;
    }
    
    private void OnDisable()
    {
        _mouseRaycaster.SurfaceClicked -= OnSurfaceClicked;
    }
    
    private void OnSurfaceClicked(Vector3 position, Vector3 normal)
    {
        _moveTarget.SetPosition(position, normal);
        _moveTarget.Show();
        
        TargetSelected?.Invoke(_moveTarget.gameObject.transform);
    }
    
    public void Hide()
    {
        _moveTarget.Hide();
    }
}
