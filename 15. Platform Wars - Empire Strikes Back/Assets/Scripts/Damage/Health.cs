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
        if (IsEmpty || delta <= 0)
            return;
        
        int newValue = Math.Max(0, _amount - delta);
        
        SetValue(newValue);
    }
    
    public void Increase(int delta)
    {
        if (IsFull || delta <= 0)
            return;
        
        int newValue = Math.Min(_amount + delta, _maxAmount);
        
        SetValue(newValue);
    }
    
    public void Reset()
    {
        SetValue(_maxAmount);
    }
    
    private void SetValue(int value)
    {
        int oldValue = _amount;
        
        _amount = value;
        
        Changed?.Invoke(oldValue, value);
    }
}
