using UnityEngine;

[RequireComponent(typeof(EventGate))]
public class Player : BaseCharacter
{
    private EventGate _eventGate;
    private Vector3 _startPosition;
    private bool _isRespawnPermitted;
    
    public void Respawn()
    {
        if (_isRespawnPermitted)
            _eventGate.Notify(GlobalEvent.Restart);
    }
    
    protected override void Awoke()
    {
        _eventGate = GetComponent<EventGate>();
        _startPosition = gameObject.transform.position;
    }
    
    protected override void Enabled()
    {
        _eventGate.Restarted += OnRestarted;
    }
    
    protected override void Disabled()
    {
        _eventGate.Restarted -= OnRestarted;
    }

    protected override void OnExplodeStarted()
    {
        _isRespawnPermitted = false;
        _eventGate.Notify(GlobalEvent.PlayerDie);
    }
    
    protected override void OnExplodeFinished()
    {
        _isRespawnPermitted = true;
    }
    
    private void OnRestarted()
    {
        IsAlive = true;
        gameObject.transform.position = _startPosition;
        
        EnableInteraction();
    }
}
