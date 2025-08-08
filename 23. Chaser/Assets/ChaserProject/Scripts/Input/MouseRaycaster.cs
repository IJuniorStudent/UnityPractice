using System;
using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    [SerializeField] private MouseInputReader _mouseInputReader;
    [SerializeField] private LayerMask _raycastLayerMask;
    
    private Camera _mainCamera;
    
    public event Action<Vector3, Vector3> SurfaceClicked;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    private void OnEnable()
    {
        _mouseInputReader.MouseLeftButtonClicked += OnMouseButtonLeftClicked;
    }
    
    private void OnDisable()
    {
        _mouseInputReader.MouseLeftButtonClicked -= OnMouseButtonLeftClicked;
    }
    
    private void OnMouseButtonLeftClicked()
    {
        if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, _raycastLayerMask) == false)
            return;
        
        SurfaceClicked?.Invoke(hitInfo.point, hitInfo.normal);
    }
}
