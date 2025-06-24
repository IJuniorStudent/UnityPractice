using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
[RequireComponent(typeof(BoxArea))]
public class AutoSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 1.0f;
    [SerializeField] private float _startRotationAngle = 180.0f;
    
    private EnemySpawner _spawner;
    private BoxArea _boxArea;
    private Quaternion _spawnRotation;
    private Coroutine _spawnRoutine;
    private WaitForSeconds _interval;
    private bool _isSpawning;
    
    private void Awake()
    {
        _spawner = GetComponent<EnemySpawner>();
        _boxArea = GetComponent<BoxArea>();
        
        _isSpawning = true;
        _interval = new WaitForSeconds(_spawnInterval);
        _spawnRotation = Quaternion.Euler(0.0f, _startRotationAngle, 0.0f);
    }
    
    private void Start()
    {
        StartSpawn();
    }
    
    public void StartSpawn()
    {
        StopSpawn();
        _spawnRoutine = StartCoroutine(SpawnEnemies());
    }
    
    public void StopSpawn()
    {
        if (_spawnRoutine == null)
            return;
        
        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }
    
    private IEnumerator SpawnEnemies()
    {
        while (_isSpawning)
        {
            SpawnEnemy();
            yield return _interval;
        }
    }
    
    private void SpawnEnemy()
    {
        Enemy enemy = _spawner.Spawn(_boxArea.GetRandomPoint(), _spawnRotation);
        enemy.ResetState();
    }
}
