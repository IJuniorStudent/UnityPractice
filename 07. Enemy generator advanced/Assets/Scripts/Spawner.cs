using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyPoolCapacity = 10;
    [SerializeField] private int _enemyPoolMaxSize = 15;
    
    private ObjectPool<Enemy> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: ReuseEnemy,
            actionOnRelease: DespawnEnemy,
            actionOnDestroy: DestroyEnemy,
            collectionCheck: true,
            defaultCapacity: _enemyPoolCapacity,
            maxSize: _enemyPoolMaxSize
        );
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
        Enemy enemy = Instantiate(_enemyPrefab);
        
        enemy.DestinationReached += OnDestinationReached;
        ActivateEnemyMove(enemy);
        
        return enemy;
    }
    
    private void ReuseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        
        ActivateEnemyMove(enemy);
    }
    
    private void DespawnEnemy(Enemy enemy)
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
        enemy.StartMove(_target);
    }
    
    private void SetInitialTransform(Transform enemyTransform)
    {
        Vector3 spawnPosition = gameObject.transform.position;
        
        enemyTransform.position = spawnPosition;
        enemyTransform.rotation = spawnPosition.VerticalLookAt(_target.position);
    }
}
