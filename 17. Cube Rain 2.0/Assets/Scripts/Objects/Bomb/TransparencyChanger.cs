using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TransparencyChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _childRenderers;
    
    private MeshRenderer _renderer;
    
    public float Value => _renderer.material.color.a;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    
    public void SetValue(float value)
    {
        Color color = _renderer.material.color;
        color.a = value;
        
        foreach (var childRenderer in _childRenderers)
            childRenderer.material.color = color;
        
        _renderer.material.color = color;
    }
}
