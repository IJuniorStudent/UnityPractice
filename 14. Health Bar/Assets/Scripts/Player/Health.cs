using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int InitialAmount = 100;
    
    [SerializeField] private int _amount = InitialAmount;
    [SerializeField] private int _maxAmount = InitialAmount;
    
    public bool IsEmpty => _amount == 0;
    public bool IsFull => _amount == _maxAmount;
    public int Amount => _amount;
    public int MaxAmount => _maxAmount;
    
    public event Action<int, int> Changed;
    
    public void Decrease(int delta)
    {
        int value = Math.Clamp(delta, 0, _amount);
        
        TryChange(-value);
    }
    
    public void Increase(int delta)
    {
        int value = Math.Clamp(delta, 0, _maxAmount - _amount);
        
        TryChange(value);
    }
    
    public void Reset()
    {
        TryChange(_maxAmount - _amount);
    }
    
    private void TryChange(int delta)
    {
        if (delta == 0)
            return;
        
        int oldValue = _amount;
        
        _amount += delta;
        Changed?.Invoke(oldValue, _amount);
    }
}
