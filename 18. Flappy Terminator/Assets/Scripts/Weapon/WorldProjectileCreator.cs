using System.Collections.Generic;
using UnityEngine;

public class WorldProjectileCreator : MonoBehaviour
{
    [SerializeField] private SequentialShooter _playerShooter;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ProjectileSpawner _playerProjectileSpawner;
    [SerializeField] private ProjectileSpawner _enemyProjectileSpawner;
    
    private Dictionary<Enemy, SequentialShooter> _enemyShootersMap;
    
    private void Awake()
    {
        _enemyShootersMap = new Dictionary<Enemy, SequentialShooter>();
    }
    
    private void OnEnable()
    {
        _enemySpawner.Created += OnEnemyCreated;
        _enemySpawner.Destroyed += OnEnemyDestroyed;
        _playerShooter.Shoot += OnPlayerShoot;
        
        foreach (var (_, shooter) in _enemyShootersMap)
            shooter.Shoot += OnEnemyShoot;
    }
    
    private void OnDisable()
    {
        _enemySpawner.Created -= OnEnemyCreated;
        _enemySpawner.Destroyed -= OnEnemyDestroyed;
        _playerShooter.Shoot -= OnPlayerShoot;
        
        foreach (var (_, shooter) in _enemyShootersMap)
            shooter.Shoot -= OnEnemyShoot;
    }
    
    private void OnPlayerShoot(Vector3 position, Quaternion rotation)
    {
        _playerProjectileSpawner.Spawn(position, rotation);
    }
    
    private void OnEnemyShoot(Vector3 position, Quaternion rotation)
    {
        _enemyProjectileSpawner.Spawn(position, rotation);
    }
    
    private void OnEnemyCreated(Enemy enemy)
    {
        if (enemy.gameObject.TryGetComponent(out SequentialShooter shooter) == false)
            return;
        
        shooter.Shoot += OnEnemyShoot;
        _enemyShootersMap[enemy] = shooter;
    }
    
    private void OnEnemyDestroyed(Enemy enemy)
    {
        if (_enemyShootersMap.TryGetValue(enemy, out SequentialShooter shooter) == false)
            return;
        
        shooter.Shoot -= OnEnemyShoot;
        _enemyShootersMap.Remove(enemy);
    }
}
