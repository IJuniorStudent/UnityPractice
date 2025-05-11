using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        
        GetComponent<InputReceiver>().MouseButtonPressed += Fire;
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
