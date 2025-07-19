using System;
using UnityEngine;

public class ResourceBaseBuilder : MonoBehaviour
{
    public event Action ResourceBaseBuilt;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ResourceBaseFoundation>(out _))
            ResourceBaseBuilt?.Invoke();
    }
}
