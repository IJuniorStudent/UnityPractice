using System;
using UnityEngine;

[RequireComponent(typeof(ForwardMover))]
[RequireComponent(typeof(AutoShooter))]
public class Enemy : BaseCharacter
{
    private ForwardMover _forwardMover;
    private AutoShooter _autoShooter;
    
    public event Action<Enemy> LifetimeEnded;
    
    public void ResetState()
    {
        IsAlive = true;
        _forwardMover.StartMove();
        EnableInteraction();
    }
    
    public void StartFire()
    {
        _autoShooter.StartFire();
    }
    
    public void StopFire()
    {
        _autoShooter.StopFire();
    }
    
    public void FinishLifetime()
    {
        LifetimeEnded?.Invoke(this);
    }
    
    protected override void Awoke()
    {
        _forwardMover = GetComponent<ForwardMover>();
        _autoShooter = GetComponent<AutoShooter>();
    }
    
    protected override void OnExplodeStarted()
    {
        _forwardMover.StopMove();
        _autoShooter.StopFire();
    }
    
    protected override void OnExplodeFinished()
    {
        LifetimeEnded?.Invoke(this);
    }
}
