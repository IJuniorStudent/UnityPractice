using System;
using UnityEngine;

public class CollectableResource : CollectorTarget
{
    [SerializeField] private int _value = 1;
    
    public int Value => _value;
    
    public event Action Reserved;
    public event Action Collected;
    public event Action Restored;
    
    public void SetStateReserved()
    {
        Reserved?.Invoke();
    }
    
    public void SetStateCollected()
    {
        Collected?.Invoke();
    }
    
    public void ResetState()
    {
        Restored?.Invoke();
    }
}
