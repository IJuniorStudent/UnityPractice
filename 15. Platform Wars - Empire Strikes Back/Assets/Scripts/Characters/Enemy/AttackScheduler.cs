using System;
using System.Collections;
using UnityEngine;

public class AttackScheduler : MonoBehaviour
{
    [SerializeField] private float _attackInterval = 1.5f;
    
    private WaitForSeconds _waitInterval;
    private Coroutine _attackRoutine;

    public event Action Started;
    public event Action Finished;
    
    private void Awake()
    {
        _waitInterval = new WaitForSeconds(_attackInterval);
    }
    
    public void StartTask()
    {
        StopAttackRoutine();
        
        _attackRoutine = StartCoroutine(Attack());
    }
    
    public void StopTask()
    {
        StopAttackRoutine();
    }
    
    private IEnumerator Attack()
    {
        Started?.Invoke();
        yield return _waitInterval;
        Finished?.Invoke();
    }
    
    private void StopAttackRoutine()
    {
        if (_attackRoutine == null)
            return;
        
        StopCoroutine(_attackRoutine);
        _attackRoutine = null;
    }
}
