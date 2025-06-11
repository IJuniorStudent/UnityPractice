using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothBarChanger : MonoBehaviour
{
    private const float ReachValueThreshold = 0.001f;
    
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillImage;
    
    private Coroutine _animationRoutine;
    private float _targetValue;
    
    public float MinValue => _slider.minValue;
    public float MaxValue => _slider.maxValue;
    
    private void Awake()
    {
        _targetValue = _slider.value;
    }
    
    public void SetValue(float value, float time)
    {
        StopAnimation();
        
        _targetValue = value;
        _animationRoutine = StartCoroutine(Animate(value, time));
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private IEnumerator Animate(float value, float time)
    {
        _fillImage.enabled = true;
        
        float deltaPerSecond = 1.0f / time;
        
        while (Mathf.Abs(value - _slider.value) > ReachValueThreshold)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, value, deltaPerSecond * Time.deltaTime);
            yield return null;
        }
        
        _fillImage.enabled = value > _slider.minValue;
    }
    
    private void StopAnimation()
    {
        if (_animationRoutine == null)
            return;
        
        StopCoroutine(_animationRoutine);
        _animationRoutine = null;
        _slider.value = _targetValue;
    }
}
