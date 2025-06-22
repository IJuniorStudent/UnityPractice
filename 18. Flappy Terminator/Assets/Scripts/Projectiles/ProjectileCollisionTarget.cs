using System;
using UnityEngine;

public class ProjectileCollisionTarget : MonoBehaviour
{
    public event Action Collided;
    
    public void InvokeProjectileCollided()
    {
        Collided?.Invoke();
    }
}
