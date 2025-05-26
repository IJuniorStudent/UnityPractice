using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AlarmDetector))]
public class AlarmVisualizer : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _volumeChangeTime = 2.0f;
    
    private AudioSource _alarmSound;
    private AlarmDetector _detector;
    private Coroutine _volumeChangeRoutine;
    
    private void Awake()
    {
        _detector = GetComponent<AlarmDetector>();
        _alarmSound = GetComponent<AudioSource>();
    }
    
    private void OnEnable()
    {
        _detector.ActivityDetected += OnActivityDetected;
        _detector.ActivityLost += OnActivityLost;
    }
    
    private void OnDisable()
    {
        _detector.ActivityDetected -= OnActivityDetected;
        _detector.ActivityLost -= OnActivityLost;
    }
    
    private void OnActivityDetected()
    {
        StopVolumeChange();
        
        if (_alarmSound.isPlaying == false)
            _alarmSound.Play();
        
        _volumeChangeRoutine = StartCoroutine(SmoothChangeVolume(1.0f));
    }
    
    private void OnActivityLost()
    {
        StopVolumeChange();
        _volumeChangeRoutine = StartCoroutine(SmoothChangeVolume(0.0f, () => _alarmSound.Stop()));
    }
    
    private void StopVolumeChange()
    {
        if (_volumeChangeRoutine == null)
            return;
        
        StopCoroutine(_volumeChangeRoutine);
        _volumeChangeRoutine = null;
    }
    
    private IEnumerator SmoothChangeVolume(float targetVolume, Action targetValueReached = null)
    {
        float maxValueDifference = 0.0f;
        
        while (Mathf.Abs(targetVolume - _alarmSound.volume) > maxValueDifference)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, Time.deltaTime / _volumeChangeTime);
            yield return null;
        }
        
        targetValueReached?.Invoke();
    }
}
