using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private Restater _restater;
    [SerializeField] private Player _player;
    [SerializeField] private AutoSpawner _enemyAutoSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private FireStartArea _fireStartArea;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private BaseImageMover[] _backgroundMovers;
    
    private void OnEnable()
    {
        _restater.RestartPressed += OnRestartPressed;
        _player.Lost += OnPlayerLost;
    }
    
    private void OnDisable()
    {
        _restater.RestartPressed -= OnRestartPressed;
        _player.Lost -= OnPlayerLost;
    }
    
    private void OnPlayerLost()
    {
        _enemyAutoSpawner.StopSpawn();
        _fireStartArea.Disable();
        
        foreach (Enemy enemy in _enemySpawner.ActiveObjects)
            enemy.StopFire();
        
        foreach (BaseImageMover mover in _backgroundMovers)
            mover.StopMove();
    }
    
    private void OnRestartPressed()
    {
        StartCoroutine(ResetGameState());
    }
    
    private IEnumerator ResetGameState()
    {
        yield return null;
        
        _enemySpawner.ReleaseActive();
        _projectileSpawner.ReleaseActive();
        
        _fireStartArea.Enable();
        _enemyAutoSpawner.StartSpawn();
        
        _player.ResetState();
        
        foreach (BaseImageMover mover in _backgroundMovers)
            mover.StartMove();
    }
}
