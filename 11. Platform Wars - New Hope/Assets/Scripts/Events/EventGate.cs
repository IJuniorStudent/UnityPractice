using System;
using UnityEngine;

public class EventGate : MonoBehaviour
{
    [SerializeField] private GlobalEventTransmitter _transmitter;
    
    public event Action PlayerDied;
    public event Action Restarted;
    
    private void Awake()
    {
        _transmitter.Received += OnReceived;
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
}
