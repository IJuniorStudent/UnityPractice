using UnityEngine;

[RequireComponent(typeof(EventGate))]
[RequireComponent(typeof(Collider2D))]
public class FireStartArea : EnemyTriggerArea
{
    private EventGate _eventGate;
    private Collider2D _collider;
    
    private void Awake()
    {
        _eventGate = GetComponent<EventGate>();
        _collider = GetComponent<Collider2D>();
    }
    
    private void OnEnable()
    {
        _eventGate.PlayerDied += OnPlayerDied;
        _eventGate.Restarted += OnRestarted;
    }
    
    private void OnDisable()
    {
        _eventGate.PlayerDied -= OnPlayerDied;
        _eventGate.Restarted -= OnRestarted;
    }
    
    protected override void OnEntered(Enemy enemy)
    {
        enemy.StartFire();
    }
    
    private void OnPlayerDied()
    {
        _collider.enabled = false;
    }
    
    private void OnRestarted()
    {
        _collider.enabled = true;
    }
}
