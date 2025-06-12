using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool _isCollided = false;
    
    public event Action Collided;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided || collision.gameObject.TryGetComponent<CollisionTarget>(out _) == false)
            return;
        
        _isCollided = true;
        Collided?.Invoke();
    }
    
    public void Reset()
    {
        _isCollided = false;
    }
}
