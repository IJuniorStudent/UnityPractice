using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EventGate))]
public class HealthAbsorber : MonoBehaviour
{
    [SerializeField] private float _activeTimeSeconds = 6.0f;
    [SerializeField] private float _cooldownTimeSeconds = 4.0f;
    [SerializeField] private float _absorbIntervalSeconds = 1.0f;
    [SerializeField] private int _healthAbsorbPerTick = 15;
    [SerializeField] private Health _health;
    [SerializeField] private AbsorbArea _area;
    
    private EventGate _eventGate;
    
    private WaitForSeconds _activeTime;
    private WaitForSeconds _cooldownTime;
    private WaitForSeconds _absorbInterval;
    private bool _isActive;
    private bool _isCooling;
    private Coroutine _areaOfEffectRoutine;
    private Coroutine _skillEffectRoutine;
    
    public event Action<float> Activated;
    public event Action<float> CooldownStarted;
    public event Action CooldownFinished;
    
    private void Awake()
    {
        _eventGate = GetComponent<EventGate>();
        
        _activeTime = new WaitForSeconds(_activeTimeSeconds);
        _cooldownTime = new WaitForSeconds(_cooldownTimeSeconds);
        _absorbInterval = new WaitForSeconds(_absorbIntervalSeconds);
    }
    
    private void OnEnable()
    {
        _eventGate.PlayerDied += OnPlayerDied;
    }
    
    private void OnDisable()
    {
        _eventGate.PlayerDied -= OnPlayerDied;
    }
    
    public void Activate()
    {
        if (_isActive || _isCooling)
            return;
        
        _areaOfEffectRoutine = StartCoroutine(UseSkill());
        _skillEffectRoutine = StartCoroutine(ActivateSkillEffect());
    }

    private IEnumerator UseSkill()
    {
        yield return ActivateSkill();
        yield return ActivateCooldown();
    }
    
    private IEnumerator ActivateSkill()
    {
        _isActive = true;
        
        Activated?.Invoke(_activeTimeSeconds);
        yield return _activeTime;
        
        _isActive = false;
    }

    private IEnumerator ActivateCooldown()
    {
        _isCooling = true;
        
        CooldownStarted?.Invoke(_cooldownTimeSeconds);
        yield return _cooldownTime;
        
        CooldownFinished?.Invoke();
        _isCooling = false;
    }
    
    private IEnumerator ActivateSkillEffect()
    {
        while (_isActive)
        {
            if (_area.TryFindClosestAliveTarget(out IDamageable target))
            {
                target.TakeDamage(_healthAbsorbPerTick);
                _health.Increase(_healthAbsorbPerTick);
            }
            
            yield return _absorbInterval;
        }
    }
    
    private void OnPlayerDied()
    {
        TerminateCoroutine(ref _areaOfEffectRoutine);
        TerminateCoroutine(ref _skillEffectRoutine);
        
        float instantCooldownTime = 0.0f;
        
        CooldownStarted?.Invoke(instantCooldownTime);
        CooldownFinished?.Invoke();
        
        _isActive = false;
        _isCooling = false;
    }
    
    private void TerminateCoroutine(ref Coroutine coroutine)
    {
        if (coroutine == null)
            return;
        
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
