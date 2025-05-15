using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _interval = 2.0f;

    private Coroutine _tickRoutine;
    private WaitForSeconds _waitForSeconds;
    
    public event Action Ticked;
    
    private void OnEnable()
    {
        _waitForSeconds = new WaitForSeconds(_interval);
        _tickRoutine = StartCoroutine(Tick());
    }

    private void OnDisable()
    {
        StopCoroutine(_tickRoutine);
        _tickRoutine = null;
    }

    private IEnumerator Tick()
    {
        while (enabled)
        {
            Ticked?.Invoke();
            yield return _waitForSeconds;
        }
    }
}
