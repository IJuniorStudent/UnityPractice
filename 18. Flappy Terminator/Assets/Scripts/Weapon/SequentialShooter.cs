using System;
using UnityEngine;

public class SequentialShooter : MonoBehaviour
{
    [SerializeField] private SingleShooter[] _shooters;
    
    private int _shooterIndex = 0;
    
    public event Action<Vector3, Quaternion> Shoot;
    
    private void OnEnable()
    {
        foreach (var shooter in _shooters)
            shooter.Shoot += OnShoot;
    }
    
    private void OnDisable()
    {
        foreach (var shooter in _shooters)
            shooter.Shoot -= OnShoot;
    }
    
    public void Fire()
    {
        _shooters[_shooterIndex].Fire();
        _shooterIndex = (_shooterIndex + 1) % _shooters.Length;
    }
    
    private void OnShoot(Vector3 position, Quaternion rotation)
    {
        Shoot?.Invoke(position, rotation);
    }
}
