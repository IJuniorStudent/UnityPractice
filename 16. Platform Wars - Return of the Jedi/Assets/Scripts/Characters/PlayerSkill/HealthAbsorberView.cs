using UnityEngine;

public class HealthAbsorberView : MonoBehaviour
{
    [SerializeField] private HealthAbsorber _absorber;
    [SerializeField] private SmoothBarChanger _abilityBar;
    [SerializeField] private SpriteRenderer _areaOfEffect;
    
    private void OnEnable()
    {
        _absorber.Activated += OnAbsorbActivated;
        _absorber.CooldownStarted += OnAbsorbCooldownStarted;
        _absorber.CooldownFinished += OnAbsorbRecharged;
    }
    
    private void OnDisable()
    {
        _absorber.Activated -= OnAbsorbActivated;
        _absorber.CooldownStarted -= OnAbsorbCooldownStarted;
        _absorber.CooldownFinished -= OnAbsorbRecharged;
    }
    
    private void OnAbsorbActivated(float absorbTime)
    {
        _areaOfEffect.enabled = true;
        _abilityBar.Show();
        _abilityBar.SetValue(_abilityBar.MinValue, absorbTime);
    }
    
    private void OnAbsorbCooldownStarted(float cooldownTime)
    {
        _areaOfEffect.enabled = false;
        _abilityBar.SetValue(_abilityBar.MaxValue, cooldownTime);
    }
    
    private void OnAbsorbRecharged()
    {
        _abilityBar.Hide();
    }
}
