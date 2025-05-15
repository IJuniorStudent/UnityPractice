using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _hueMin = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _hueMax = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _saturationMin = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _saturationMax = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _valueMin = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)] private float _valueMax = 1.0f;
    
    private Material _material = null;
    
    private void Awake()
    {
        if (TryGetComponent(out MeshRenderer meshRenderer))
            _material = meshRenderer.material;
    }

    private void OnEnable()
    {
        Randomize();
    }
    
    public void Randomize()
    {
        if (_material)
            _material.color = GetRandomColor();
    }
    
    private Color GetRandomColor()
    {
        float hue = Random.Range(_hueMin, _hueMax);
        float saturation = Random.Range(_saturationMin, _saturationMax);
        float value = Random.Range(_valueMin, _valueMax);
        
        return Color.HSVToRGB(hue, saturation, value);
    }
}
