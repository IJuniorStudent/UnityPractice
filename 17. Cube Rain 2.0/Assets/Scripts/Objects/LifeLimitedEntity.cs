using System;
using UnityEngine;

public abstract class LifeLimitedEntity : MonoBehaviour
{
    [SerializeField] protected float LifetimeMin = 2.0f;
    [SerializeField] protected float LifetimeMax = 5.0f;
    
    public event Action<LifeLimitedEntity> LifetimeExpired;
    
    protected void InvokeLifetimeExpired(LifeLimitedEntity entity)
    {
        LifetimeExpired?.Invoke(entity);
    }
    
    public abstract void Reset();
}
