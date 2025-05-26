using System;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Raycaster : MonoBehaviour
{
    private Camera _mainCamera;
    private InputReceiver _receiver;
    
    public event Action<Vector3> MoveTargetSelected;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _receiver = GetComponent<InputReceiver>();
    }
    
    private void OnEnable()
    {
        _receiver.MouseButtonClicked += OnMouseButtonClicked;
    }
    
    private void OnDisable()
    {
        _receiver.MouseButtonClicked -= OnMouseButtonClicked;
    }
    
    private void OnMouseButtonClicked()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;
        
        MoveTargetSelected?.Invoke(hit.point);
    }
}
