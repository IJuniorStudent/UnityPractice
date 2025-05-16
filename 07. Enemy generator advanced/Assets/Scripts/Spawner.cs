using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpawnDirector))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Route _route;
    [SerializeField] private int _enemyPoolCapacity = 10;
    [SerializeField] private int _enemyPoolMaxSize = 15;
    
    private ObjectPool<Enemy> _pool;
    
    private void Awake()
    {
        var director = GetComponent<SpawnDirector>();
        director.RegisterSpawner(this);
        
        _pool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: ReuseEnemy,
            actionOnRelease: ReleaseEnemy,
            actionOnDestroy: DestroyEnemy,
            collectionCheck: true,
            defaultCapacity: _enemyPoolCapacity,
            maxSize: _enemyPoolMaxSize
        );
    }
    
    private void OnDrawGizmos()
    {
        _route.DebugDraw();
    }
    
    public void SpawnEnemy()
    {
        _pool.Get();
    }
    
    private void OnDestinationReached(Enemy enemy)
    {
        _pool.Release(enemy);
    }
    
    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefab);
        
        enemy.DestinationReached += OnDestinationReached;
        ActivateEnemyMove(enemy);
        
        return enemy;
    }
    
    private void ReuseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        
        ActivateEnemyMove(enemy);
    }
    
    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    
    private void DestroyEnemy(Enemy enemy)
    {
        enemy.DestinationReached -= OnDestinationReached;
        Destroy(enemy.gameObject);
    }
    
    private void ActivateEnemyMove(Enemy enemy)
    {
        SetInitialTransform(enemy.gameObject.transform);
        enemy.StartMove(_route.Destination);
    }
    
    private void SetInitialTransform(Transform target)
    {
        target.position = _route.Start;
        target.rotation = _route.CalculateVerticalRotation();
    }
}
