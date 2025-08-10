using System;
using UnityEngine;

public class PayloadLauncher : MonoBehaviour
{
    private bool _isCollided;
    
    public event Action Launched;
    
    private void OnCollisionEnter(Collision other)
    {
        if (_isCollided)
            return;
        
        Launched?.Invoke();
        _isCollided = true;
    }
    
    public void ResetState()
    {
        _isCollided = false;
    }
}
