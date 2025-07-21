using System;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    private CollectableResource _targetResource;
    
    public event Action<CollectableResource> ResourceCollected;
    public event Action<ResourceBase> ResourceDelivered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ICollectorTarget target) == false)
            return;
        
        switch (target)
        {
            case CollectableResource resource:
                TryObtainResource(resource);
                break;
            
            case ResourceBase resourceBase:
                DeliverResource(resourceBase);
                break;
        }
    }
    
    public void SetCollectTarget(CollectableResource resource)
    {
        _targetResource = resource;
    }
    
    private void TryObtainResource(CollectableResource resource)
    {
        if (resource == _targetResource)
            ResourceCollected?.Invoke(resource);
    }
    
    private void DeliverResource(ResourceBase resourceBase)
    {
        ResourceDelivered?.Invoke(resourceBase);
    }
}
