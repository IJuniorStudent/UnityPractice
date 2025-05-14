using System;
using System.Collections.Generic;
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
    
    public List<Cube> LocateCubes(Vector3 point, float radius)
    {
        RaycastHit[] hits = Physics.SphereCastAll(point, radius, Vector3.up, 0.0f);
        
        List<Cube> cubes = new ();
        
        foreach (var hit in hits)
            if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                cubes.Add(cube);
        
        return cubes;
    }
    
    private void OnMouseButtonPressed()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit) == false)
            return;
        
        if (hit.collider.gameObject.TryGetComponent(out Cube cube))
            Touched?.Invoke(cube);
    }
}
