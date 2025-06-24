using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    
    public event Action<Projectile> Collided;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ProjectileCollisionTarget target) == false)
            return;
        
        Collided?.Invoke(this);
        target.InvokeProjectileCollided();
    }
}
