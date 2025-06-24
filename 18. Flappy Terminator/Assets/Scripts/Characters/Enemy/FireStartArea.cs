using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FireStartArea : EnemyTriggerArea
{
    private Collider2D _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    
    public void Enable()
    {
        _collider.enabled = true;
    }
    
    public void Disable()
    {
        _collider.enabled = false;
    }
    
    protected override void OnEntered(Enemy enemy)
    {
        enemy.StartFire();
    }
}
