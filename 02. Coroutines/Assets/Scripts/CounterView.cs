using UnityEngine;

[RequireComponent(typeof(Counter))]
public class CounterView : MonoBehaviour
{
    private Counter _counter;
    
    private void Awake()
    {
        _counter = GetComponent<Counter>();
    }
    
    private void OnEnable()
    {
        _counter.ValueChanged += OnValueChanged;
    }
    
    private void OnDisable()
    {
        _counter.ValueChanged -= OnValueChanged;
    }
    
    private void OnValueChanged(int value)
    {
        Debug.Log(value);
    }
}
