using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SequentialShooter))]
public class AutoShooter : MonoBehaviour
{
    [SerializeField] private float _shootInterval = 1.0f;
    
    private SequentialShooter _shooter;
    private Coroutine _shootingRoutine;
    private WaitForSeconds _interval;
    
    private void Awake()
    {
        _shooter = GetComponent<SequentialShooter>();
        _interval = new WaitForSeconds(_shootInterval);
    }
    
    public void StartFire()
    {
        StopFire();
        
        _shootingRoutine = StartCoroutine(Fire());
    }
    
    public void StopFire()
    {
        if (_shootingRoutine == null)
            return;
        
        StopCoroutine(_shootingRoutine);
        _shootingRoutine = null;
    }
    
    private IEnumerator Fire()
    {
        while (enabled)
        {
            _shooter.Fire();
            yield return _interval;
        }
    }
}
