using System;
using UnityEngine;

[RequireComponent(typeof(ForwardMover))]
[RequireComponent(typeof(AutoShooter))]
[RequireComponent(typeof(EventGate))]
public class Enemy : BaseCharacter
{
    private ForwardMover _forwardMover;
    private AutoShooter _autoShooter;
    private EventGate _eventGate;
    
    public event Action<Enemy> Started;
    public event Action<Enemy> LifetimeEnded;
    
    private void Start()
    {
        Started?.Invoke(this);
    }
    
    public void Initialize(ProjectileSpawner projectileSpawner, GlobalEventTransmitter transmitter)
    {
        _autoShooter.Initialize(projectileSpawner);
        _eventGate.Initialize(transmitter);
    }
    
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
    
    public void FinishLifetime()
    {
        LifetimeEnded?.Invoke(this);
    }
    
    protected override void Awoke()
    {
        _forwardMover = GetComponent<ForwardMover>();
        _autoShooter = GetComponent<AutoShooter>();
        _eventGate = GetComponent<EventGate>();
    }
    
    protected override void Enabled()
    {
        _eventGate.PlayerDied += OnPlayerDied;
    }
    
    protected override void Disabled()
    {
        _eventGate.PlayerDied -= OnPlayerDied;
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
    
    private void OnPlayerDied()
    {
        _autoShooter.StopFire();
    }
}
