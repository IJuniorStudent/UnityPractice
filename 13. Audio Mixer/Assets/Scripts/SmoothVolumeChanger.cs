using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothVolumeChanger : MonoBehaviour
{
    private const float DisabledVolumeValue = -80.0f;
    private const float ZeroVolumeValue = 0.0001f;
    private const float PowerToValueConverter = 20.0f;

    private const float SliderMinValue = 0.0f;
    private const float SliderMaxValue = 1.0f;
    
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _controlParameterName;
    
    private Slider _slider;
    
    public void Awake()
    {
        _slider = GetComponent<Slider>();
        
        _slider.minValue = SliderMinValue;
        _slider.maxValue = SliderMaxValue;
        _slider.value = Mathf.Clamp01(_slider.value);
    }
    
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
    
    private void OnSliderValueChanged(float value)
    {
        _audioMixer.SetFloat(_controlParameterName, RecalculateVolume(value));
    }
    
    private float RecalculateVolume(float value)
    {
        return value > ZeroVolumeValue ? Mathf.Log10(value) * PowerToValueConverter : DisabledVolumeValue;
    }
}
