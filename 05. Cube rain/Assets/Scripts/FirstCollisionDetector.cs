using System;
using UnityEngine;

public class FirstCollisionDetector : MonoBehaviour
{
    public event Action<FirstCollisionDetector> Collided;

    private bool _hasCollided = false;

    private void OnEnable()
    {
        _hasCollided = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent<CollisionTarget>(out _))
        {
            _hasCollided = true;
            Collided?.Invoke(this);
        }
    }
}
