public class ProjectileSpawner : GenericSpawner<Projectile>
{
    protected override void OnObjectCreate(Projectile projectile)
    {
        projectile.Collided += OnProjectileCollided;
    }

    protected override void OnObjectDestroy(Projectile projectile)
    {
        projectile.Collided -= OnProjectileCollided;
    }
    
    private void OnProjectileCollided(Projectile projectile)
    {
        Despawn(projectile);
    }
}
