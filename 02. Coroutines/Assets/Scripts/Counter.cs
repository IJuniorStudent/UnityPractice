using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _interval = 0.5f;
    
    private const int MouseButtonLeft = 0;
    
    private int _value = 0;
    private Coroutine _valueChanger = null;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButtonLeft))
            OnLeftMouseButtonDown();
    }
    
    private void OnLeftMouseButtonDown()
    {
        if (_valueChanger != null)
        {
            StopCoroutine(_valueChanger);
            _valueChanger = null;
            return;
        }
        
        _valueChanger = StartCoroutine(DisplayCounter());
    }
    
    private IEnumerator DisplayCounter()
    {
        while (true)
        {
            Debug.Log($"Value: {_value++}");
            yield return new WaitForSeconds(_interval);
        }
    }
}
