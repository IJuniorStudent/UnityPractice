using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Counter : MonoBehaviour
{
    private const float ChangeInterval = 0.5f;
    
    private InputReceiver _receiver;
    private Coroutine _valueChanger;
    private WaitForSeconds _timeWait;
    private int _value;
    
    public event Action<int> ValueChanged;
    
    private void Awake()
    {
        _receiver = GetComponent<InputReceiver>();
        _timeWait = new WaitForSeconds(ChangeInterval);
    }
    
    private void OnEnable()
    {
        _receiver.MouseButtonPressed += OnMouseButtonPressed;
    }
    
    private void OnDisable()
    {
        _receiver.MouseButtonPressed -= OnMouseButtonPressed;
    }
    
    private void OnMouseButtonPressed()
    {
        if (_valueChanger != null)
        {
            StopCoroutine(_valueChanger);
            _valueChanger = null;
            return;
        }
        
        _valueChanger = StartCoroutine(IncreaseValue());
    }
    
    private IEnumerator IncreaseValue()
    {
        while (enabled)
        {
            ValueChanged?.Invoke(++_value);
            yield return _timeWait;
        }
    }
}
