using System;
using UnityEngine;

public class FirstCollisionDetector : MonoBehaviour
{
    private bool _hasCollided = false;
    
    public event Action Collided;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent<CollisionTarget>(out _))
        {
            _hasCollided = true;
            Collided?.Invoke();
        }
    }

    public void Reset()
    {
        _hasCollided = false;
    }
}
