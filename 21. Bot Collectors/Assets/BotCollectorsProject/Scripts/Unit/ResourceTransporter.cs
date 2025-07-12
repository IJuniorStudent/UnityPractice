using UnityEngine;

public class ResourceTransporter : MonoBehaviour
{
    [SerializeField] private Transform _attachPivot;
    
    private Transform _attachedResource;
    
    public bool TryAttach(Transform resource)
    {
        if (_attachedResource != null)
            return false;
        
        resource.SetParent(_attachPivot);
        resource.localPosition = Vector3.zero;
        _attachedResource = resource;
        
        return true;
    }
    
    public bool TryDetach()
    {
        if (_attachedResource == null)
            return false;
        
        _attachedResource.SetParent(null);
        _attachedResource = null;
        
        return true;
    }
}
