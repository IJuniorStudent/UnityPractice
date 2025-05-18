using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] Projectile _projectile;
    [SerializeField] private float _projectileSpeed = 100.0f;
    [SerializeField] float _shootInterval = 1.0f;
    [SerializeField] private Transform _target;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 100;
    
    private ObjectPool<Projectile> _pool;
    private WaitForSeconds _shootWait;
    
    private void Start()
    {
        _pool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: ReuseProjectile,
            actionOnRelease: DespawnProjectile,
            actionOnDestroy: DestroyProjectile,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
        
        _shootWait = new WaitForSeconds(_shootInterval);
        
        StartCoroutine(ShootTarget());
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(_projectile);
        
        projectile.Initialize(transform.position + transform.forward, _target.position, transform.forward * _projectileSpeed);
        
        projectile.Collided += OnCollided;
        projectile.MaxDistanceReached += OnMaxDistanceReached;
        
        return projectile;
    }

    private void ReuseProjectile(Projectile projectile)
    {
        projectile.Initialize(transform.position + transform.forward, _target.position, transform.forward * _projectileSpeed);
        projectile.gameObject.SetActive(true);
    }

    private void DespawnProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void DestroyProjectile(Projectile projectile)
    {
        projectile.Collided -= OnCollided;
        projectile.MaxDistanceReached -= OnMaxDistanceReached;
        
        Destroy(projectile.gameObject);
    }
    
    private void OnCollided(Projectile projectile)
    {
        _pool.Release(projectile);
    }

    private void OnMaxDistanceReached(Projectile projectile)
    {
        _pool.Release(projectile);
    }
    
    private IEnumerator ShootTarget()
    {
        while (enabled)
        {
            _pool.Get();
            yield return _shootWait;
        }
    }
}
