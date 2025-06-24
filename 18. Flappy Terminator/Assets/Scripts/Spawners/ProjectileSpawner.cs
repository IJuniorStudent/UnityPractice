public class ProjectileSpawner : GenericSpawner<Projectile>
{
    protected override void OnObjectCreate(Projectile projectile)
    {
        projectile.Collided += Despawn;
    }

    protected override void OnObjectDestroy(Projectile projectile)
    {
        projectile.Collided -= Despawn;
    }
}
