using System;
using UnityEngine;

public class Player : BaseCharacter
{
    private Vector3 _startPosition;
    private bool _isRespawnPermitted;
    
    public event Action Lost;
    
    public bool CanBeRespawned => _isRespawnPermitted;
    
    public void ResetState()
    {
        IsAlive = true;
        gameObject.transform.position = _startPosition;
        EnableInteraction();
    }
    
    protected override void Awoke()
    {
        _startPosition = gameObject.transform.position;
    }

    protected override void OnExplodeStarted()
    {
        _isRespawnPermitted = false;
        Lost?.Invoke();
    }
    
    protected override void OnExplodeFinished()
    {
        _isRespawnPermitted = true;
    }
}
