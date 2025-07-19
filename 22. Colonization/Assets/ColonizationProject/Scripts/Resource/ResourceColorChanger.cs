using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ResourceColorChanger : MonoBehaviour
{
    [SerializeField] private CollectableResource _resource;
    [SerializeField] private Color _reservedColor = Color.yellow;
    [SerializeField] private Color _collectedColor = Color.green;
    
    private MeshRenderer _renderer;
    private Color _defaultColor;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColor = _renderer.material.color;
    }
    
    private void OnEnable()
    {
        _resource.Reserved += OnResourceReserved;
        _resource.Collected += OnResourceCollected;
        _resource.Restored += OnResourceRestored;
    }

    private void OnDisable()
    {
        _resource.Reserved -= OnResourceReserved;
        _resource.Collected -= OnResourceCollected;
        _resource.Restored -= OnResourceRestored;
    }
    
    private void OnResourceReserved()
    {
        _renderer.material.color = _reservedColor;
    }
    
    private void OnResourceCollected()
    {
        _renderer.material.color = _collectedColor;
    }
    
    private void OnResourceRestored()
    {
        _renderer.material.color = _defaultColor;
    }
}
