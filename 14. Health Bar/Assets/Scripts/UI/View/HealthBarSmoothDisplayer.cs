using System.Collections;
using UnityEngine;

public class HealthBarSmoothDisplayer : HealthBarDisplayer
{
    private const float ReachValueThreshold = 0.0001f;
    private const int MinHealthValue = 0;
    
    [SerializeField] private float _valueChangePerFrame = 0.01f;
    
    private Coroutine _barAnimateRoutine;
    private float _targetValue;
    
    protected override void OnHealthChanged(int oldValue, int newValue)
    {
        StopAnimation();
        
        _targetValue = (float)newValue / Health.MaxAmount;
        _barAnimateRoutine = StartCoroutine(AnimateChanges(newValue));
    }
    
    private IEnumerator AnimateChanges(int newValue)
    {
        if (newValue > MinHealthValue)
            UpdateBarVisibleState();
        
        while (Mathf.Abs(_targetValue - Slider.value) > ReachValueThreshold)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, _targetValue, _valueChangePerFrame);
            yield return null;
        }
        
        if (newValue == MinHealthValue)
            UpdateBarVisibleState();
    }
    
    private void StopAnimation()
    {
        if (_barAnimateRoutine == null)
            return;
        
        StopCoroutine(_barAnimateRoutine);
        _barAnimateRoutine = null;
    }
    
    private void UpdateBarVisibleState()
    {
        BarImage.enabled = !Health.IsEmpty;
    }
}
