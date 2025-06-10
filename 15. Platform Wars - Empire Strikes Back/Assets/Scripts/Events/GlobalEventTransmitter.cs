using System;
using UnityEngine;

public class GlobalEventTransmitter : MonoBehaviour
{
    public event Action<GlobalEvent> Received;
    
    public void Send(GlobalEvent globalEventId)
    {
        Received?.Invoke(globalEventId);
    }
}
