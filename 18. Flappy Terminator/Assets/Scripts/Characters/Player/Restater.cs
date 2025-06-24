using System;
using UnityEngine;

public class Restater : MonoBehaviour
{
    [SerializeField] private InputReceiver _inputReceiver;
    [SerializeField] private Player _player;
    
    public event Action RestartPressed;
    
    private void OnEnable()
    {
        _inputReceiver.FirePressed += OnFirePressed;
    }
    
    private void OnDisable()
    {
        _inputReceiver.FirePressed -= OnFirePressed;
    }
    
    private void OnFirePressed()
    {
        if (_player.IsAlive == false && _player.CanBeRespawned)
            RestartPressed?.Invoke();
    }
}
