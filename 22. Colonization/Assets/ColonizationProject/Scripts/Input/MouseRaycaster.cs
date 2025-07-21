using System;
using UnityEngine;

[RequireComponent(typeof(MouseInput))]
public class MouseRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    
    private Camera _mainCamera;
    private MouseInput _mouseInput;
    
    public event Action<IRaycastTarget, Vector3> ClickedOnTarget;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _mouseInput = GetComponent<MouseInput>();
    }
    
    private void OnEnable()
    {
        _mouseInput.LeftClicked += OnMouseLeftClicked;
    }
    
    private void OnDisable()
    {
        _mouseInput.LeftClicked -= OnMouseLeftClicked;
    }
    
    private void OnMouseLeftClicked()
    {
        if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, _layerMask) == false)
            return;
        
        if (hitInfo.collider.gameObject.TryGetComponent(out IRaycastTarget target) == false)
            return;
        
        ClickedOnTarget?.Invoke(target, hitInfo.point);
    }
}
