using System;
using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private GlobalEventTransmitter _transmitter;

    private bool _isSubscribed = false;
    
    public event Action PlayerDied;
    public event Action Restarted;
    
    private void OnEnable()
    {
        Subscribe();
    }
    
    private void OnDisable()
    {
        Unsubscribe();
    }
    
    public void Initialize(GlobalEventTransmitter transmitter)
    {
        _transmitter = transmitter;
        Subscribe();
    }
    
    public void Notify(GlobalEvent globalEventId)
    {
        _transmitter.Send(globalEventId);
    }
    
    private void OnReceived(GlobalEvent globalEventId)
    {
        switch (globalEventId)
        {
            case GlobalEvent.PlayerDie:
                PlayerDied?.Invoke();
                break;
            
            case GlobalEvent.Restart:
                Restarted?.Invoke();
                break;
        }
    }
    
    private void Subscribe()
    {
        if (_transmitter == null || _isSubscribed)
            return;
        
        _isSubscribed = true;
        _transmitter.Received += OnReceived;
    }
    
    private void Unsubscribe()
    {
        if (_transmitter == null || _isSubscribed == false)
            return;
        
        _isSubscribed = false;
        _transmitter.Received -= OnReceived;
    }
}
