using UnityEngine;

public class CameraFacingRotator : MonoBehaviour
{
    private Camera _mainCamera;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    private void LateUpdate()
    {
        Quaternion cameraRotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.forward, cameraRotation * Vector3.up);
    }
}
