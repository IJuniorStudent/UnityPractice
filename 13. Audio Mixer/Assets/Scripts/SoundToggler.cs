using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundToggler : MonoBehaviour
{
    private const float VolumeMinValue = 0.0f;
    private const float VolumeMaxValue = 1.0f;
    
    private Button _button;
    private bool _isPlaying = true;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
    
    private void OnButtonClick()
    {
        AudioListener.volume = _isPlaying ? VolumeMinValue : VolumeMaxValue;
        _isPlaying = !_isPlaying;
    }
}
