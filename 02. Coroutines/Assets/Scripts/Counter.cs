using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Counter : MonoBehaviour
{
    [SerializeField] private float _changeInterval = 0.5f;
    
    public event Action<int> ValueChanged;
    
    private InputReceiver _receiver;
    private Coroutine _valueChanger = null;
    private int _value = 0;
    
    private void Awake()
    {
        _receiver = GetComponent<InputReceiver>();
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
        ToggleTimer();
    }
    
    private void ToggleTimer()
    {
        if (_valueChanger == null)
            EnableTimer();
        else
            DisableTimer();
    }
    
    private void EnableTimer()
    {
        _valueChanger = StartCoroutine(IncreaseValue());
    }

    private void DisableTimer()
    {
        StopCoroutine(_valueChanger);
        _valueChanger = null;
    }
    
    private IEnumerator IncreaseValue()
    {
        var timeWait = new WaitForSeconds(_changeInterval);
        
        while (true)
        {
            ValueChanged?.Invoke(_value++);
            yield return timeWait;
        }
    }
}
