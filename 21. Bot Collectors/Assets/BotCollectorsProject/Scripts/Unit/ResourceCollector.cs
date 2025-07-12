using System;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private Transform _resourceAttachPivot;

    private CollectableResource _targetResource;
    private CollectableResource _collectedResource;

    public event Action<ResourceCollector> ResourceCollected;
    public event Action<ResourceCollector> ResourceStored;
    
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
        _targetResource.Reserve();
    }
    
    private void TryObtainResource(CollectableResource resource)
    {
        if (resource != _targetResource || _collectedResource != null || resource.TryAttach(_resourceAttachPivot) == false)
            return;
        
        _collectedResource = resource;
        ResourceCollected?.Invoke(this);
    }
    
    private void TryStoreResource(ResourceStorage storage)
    {
        if (_collectedResource == null || _collectedResource.TryDetach() == false)
            return;
        
        storage.Store(_collectedResource.Value);
        
        _collectedResource.Store();
        _collectedResource.Free();
        _collectedResource = null;
        _targetResource = null;
        
        ResourceStored?.Invoke(this);
    }
}
