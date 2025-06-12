using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorRandomizer : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _hueMin = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _hueMax = 1.0f;
    [SerializeField] private float _saturation = 1.0f;
    [SerializeField] private float _value = 1.0f;
    
    private MeshRenderer _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    
    public void Change()
    {
        _renderer.material.color = Color.HSVToRGB(Random.Range(_hueMin, _hueMax), _saturation, _value);
    }
}
