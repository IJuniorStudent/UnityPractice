using System;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Gun : MonoBehaviour
{
    private Camera _mainCamera;
    private InputReceiver _receiver;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _receiver = GetComponent<InputReceiver>();
    }
    
    private void OnEnable()
    {
        _receiver.MouseButtonPressed += Fire;
    }
    
    private void OnDisable()
    {
        _receiver.MouseButtonPressed -= Fire;
    }
    
    private void Fire()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;
        
        if (hit.collider.gameObject.TryGetComponent<Exploder>(out var exploder))
            exploder.Explode();
    }
}
