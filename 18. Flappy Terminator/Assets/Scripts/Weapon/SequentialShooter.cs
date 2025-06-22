using UnityEngine;

public class SequentialShooter : MonoBehaviour
{
    [SerializeField] private SingleShooter[] _shooters;
    
    private int _shooterIndex = 0;
    
    public void Initialize(ProjectileSpawner projectileSpawner)
    {
        foreach (var shooter in _shooters)
            shooter.Initialize(projectileSpawner);
    }
    
    public void Fire()
    {
        _shooters[_shooterIndex].Fire();
        _shooterIndex = (_shooterIndex + 1) % _shooters.Length;
    }
}
