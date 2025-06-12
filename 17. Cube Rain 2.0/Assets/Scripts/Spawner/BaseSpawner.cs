using System;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public event Action ObjectSpawned;
    public event Action ObjectCreated;
    public event Action ObjectActivated;
    public event Action ObjectDeactivated;
    
    protected void InvokeObjectSpawned()
    {
        ObjectSpawned?.Invoke();
    }
    
    protected void InvokeObjectCreated()
    {
        ObjectCreated?.Invoke();
    }
    
    protected void InvokeObjectActivated()
    {
        ObjectActivated?.Invoke();
    }
    
    protected void InvokeObjectDeactivated()
    {
        ObjectDeactivated?.Invoke();
    }
}
