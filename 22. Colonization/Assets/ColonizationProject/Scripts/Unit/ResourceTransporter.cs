using UnityEngine;

public class ResourceTransporter : MonoBehaviour
{
    [SerializeField] private Transform _attachPivot;
    
    private CollectableResource _attachedResource;
    
    public bool IsAttached => _attachedResource != null;
    
    public bool TryAttach(CollectableResource resource)
    {
        if (_attachedResource != null)
            return false;
        
        resource.gameObject.transform.SetParent(_attachPivot);
        resource.gameObject.transform.localPosition = Vector3.zero;
        _attachedResource = resource;
        
        return true;
    }
    
    public bool TryDetach(out CollectableResource resource)
    {
        resource = null;
        
        if (_attachedResource == null)
            return false;
        
        _attachedResource.gameObject.transform.SetParent(null);
        
        resource = _attachedResource;
        _attachedResource = null;
        
        return true;
    }
}
