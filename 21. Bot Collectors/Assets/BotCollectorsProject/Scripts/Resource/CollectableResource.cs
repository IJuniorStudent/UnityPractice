using System;
using UnityEngine;

public class CollectableResource : CollectorTarget
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private int _value = 1;
    
    private bool _isReserved;
    private bool _isAttached;
    private Color _defaultColor;
    
    public bool CanBeCollected => _isAttached == false && _isReserved == false;
    public int Value => _value;
    
    public event Action<CollectableResource> Stored;
    
    private void Awake()
    {
        _defaultColor = _renderer.material.color;
    }
    
    public bool TryAttach(Transform parent)
    {
        if (_isAttached)
            return false;
        
        _isAttached = true;
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        _renderer.material.color = Color.green;
        
        return true;
    }
    
    public bool TryDetach()
    {
        if (_isAttached == false)
            return false;
        
        _isReserved = false;
        _isAttached = false;
        transform.SetParent(null);
        
        return true;
    }
    
    public void Store()
    {
        Stored?.Invoke(this);
    }
    
    public void Reserve()
    {
        _isReserved = true;
        _renderer.material.color = Color.yellow;
    }
    
    public void Free()
    {
        _isReserved = false;
        _renderer.material.color = _defaultColor;
    }
}
