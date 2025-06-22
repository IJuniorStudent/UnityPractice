public class DespawnArea : EnemyTriggerArea
{
    protected override void OnEntered(Enemy enemy)
    {
        enemy.FinishLifetime();
    }
}
