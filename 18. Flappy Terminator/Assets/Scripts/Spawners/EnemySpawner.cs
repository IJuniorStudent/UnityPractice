using UnityEngine;

public class EnemySpawner : GenericSpawner<Enemy>
{
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private GlobalEventTransmitter _transmitter;
    
    protected override void OnObjectCreate(Enemy enemy)
    {
        enemy.LifetimeEnded += OnEnemyLifetimeEnded;
        enemy.Started += OnEnemyStarted;
    }
    
    protected override void OnObjectDestroy(Enemy enemy)
    {
        enemy.LifetimeEnded -= OnEnemyLifetimeEnded;
        enemy.Started -= OnEnemyStarted;
    }
    
    private void OnEnemyStarted(Enemy enemy)
    {
        enemy.Initialize(_projectileSpawner, _transmitter);
    }
    
    private void OnEnemyLifetimeEnded(Enemy enemy)
    {
        Despawn(enemy);
    }
}
