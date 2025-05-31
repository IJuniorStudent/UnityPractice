using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int InitialHealth = 100;
    
    [SerializeField] private int _health = InitialHealth;
    [SerializeField] private int _maxHealth = InitialHealth;
    
    public bool IsEmpty => _health == 0;
    public bool IsFull => _health == _maxHealth;
    
    public event Action<int, int> Changed;
    
    public void Decrease(int delta)
    {
        int value = Math.Clamp(delta, 0, _health);
        
        TryChange(-value);
    }
    
    public void Increase(int delta)
    {
        int value = Math.Clamp(delta, 0, _maxHealth - _health);
        
        TryChange(value);
    }
    
    public void Reset()
    {
        TryChange(_maxHealth - _health);
    }
    
    private void TryChange(int delta)
    {
        if (delta == 0)
            return;
        
        int oldValue = _health;
        
        _health += delta;
        Changed?.Invoke(oldValue, _health);
    }
}
