using System;
using UnityEngine;

[RequireComponent(typeof(ResourceTransporter))]
public class ResourceCollector : MonoBehaviour
{
    private CollectableResource _targetResource;
    private ResourceTransporter _transporter;
    
    public event Action<CollectableResource> ResourceCollected;
    public event Action<CollectableResource> ResourceDelivered;
    
    private void Awake()
    {
        _transporter = GetComponent<ResourceTransporter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CollectorTarget target) == false)
            return;

        switch (target)
        {
            case CollectableResource resource:
                TryObtainResource(resource);
                break;
            
            case ResourceStorage storage:
                TryStoreResource(storage);
                break;
        }
    }
    
    public void SetCollectTarget(CollectableResource resource)
    {
        _targetResource = resource;
    }
    
    private void TryObtainResource(CollectableResource resource)
    {
        if (resource != _targetResource || _transporter.TryAttach(resource.transform) == false)
            return;
        
        resource.SetStateCollected();
        ResourceCollected?.Invoke(resource);
    }
    
    private void TryStoreResource(ResourceStorage storage)
    {
        if (_transporter.TryDetach() == false)
            return;
        
        storage.Store(_targetResource.Value);
        ResourceDelivered?.Invoke(_targetResource);
        
        _targetResource = null;
    }
}
