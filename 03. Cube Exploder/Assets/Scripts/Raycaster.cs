using System;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Raycaster : MonoBehaviour
{
    private Camera _mainCamera;
    private InputReceiver _receiver;
    
    public event Action<Cube> Touched;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _receiver = GetComponent<InputReceiver>();
    }
    
    private void OnEnable()
    {
        _receiver.MouseButtonPressed += OnMouseButtonPressed;
    }
    
    private void OnDisable()
    {
        _receiver.MouseButtonPressed -= OnMouseButtonPressed;
    }
    
    private void OnMouseButtonPressed()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;
        
        if (hit.collider.gameObject.TryGetComponent<Cube>(out var cube))
            Touched?.Invoke(cube);
    }
}
