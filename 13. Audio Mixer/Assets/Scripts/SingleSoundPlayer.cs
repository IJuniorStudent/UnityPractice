using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class SingleSoundPlayer : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
