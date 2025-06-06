using UnityEngine;
using UnityEngine.Audio;

public class MasterAudio : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string EffectsVolume = "EffectsVolume";
    private const string BackgroundMusicVolume = "BackgroundMusicVolume";

    private const float DisabledVolumeValue = -80.0f;
    
    [SerializeField] private AudioMixerGroup _masterGroup;
    [SerializeField] private AudioMixerGroup _effectsGroup;
    [SerializeField] private AudioMixerGroup _backgroundMusicGroup;
    
    private bool _isSoundEnabled;
    private float _masterVolumeValue;
    
    private void Awake()
    {
        _isSoundEnabled = true;
    }
    
    public void ToggleVolumeEnabled()
    {
        float targetVolumeValue;
        
        if (_isSoundEnabled)
        {
            targetVolumeValue = DisabledVolumeValue;
            _isSoundEnabled = false;
        }
        else
        {
            targetVolumeValue = _masterVolumeValue;
            _isSoundEnabled = true;
        }
        
        _masterGroup.audioMixer.SetFloat(MasterVolume, targetVolumeValue);
    }
    
    public void SetMasterVolume(float volume)
    {
        _masterGroup.audioMixer.SetFloat(MasterVolume, RecalculateVolume(volume));
        _masterVolumeValue = volume;
    }
    
    public void SetEffectsVolume(float volume)
    {
        _masterGroup.audioMixer.SetFloat(EffectsVolume, RecalculateVolume(volume));
    }
    
    public void SetBackgroundMusicVolume(float volume)
    {
        _masterGroup.audioMixer.SetFloat(BackgroundMusicVolume, RecalculateVolume(volume));
    }
    
    public void PlaySound(AudioSource source)
    {
        source.PlayOneShot(source.clip);
    }
    
    private float RecalculateVolume(float value)
    {
        float zeroVolumeValue = 0.0001f;
        
        return value > zeroVolumeValue ? Mathf.Log10(value) * 20.0f : DisabledVolumeValue;
    }
}
