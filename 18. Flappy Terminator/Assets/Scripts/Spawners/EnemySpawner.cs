public class EnemySpawner : GenericSpawner<Enemy>
{
    protected override void OnObjectCreate(Enemy enemy)
    {
        enemy.LifetimeEnded += Despawn;
    }
    
    protected override void OnObjectDestroy(Enemy enemy)
    {
        enemy.LifetimeEnded -= Despawn;
    }
}
