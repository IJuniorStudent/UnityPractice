using UnityEngine;

public abstract class HealthDisplayer : MonoBehaviour
{
    [SerializeField] protected Health Health;
    
    private void OnEnable()
    {
        Health.Changed += OnHealthChanged;
    }
    
    private void OnDisable()
    {
        Health.Changed -= OnHealthChanged;
    }
    
    protected abstract void OnHealthChanged(int oldValue, int newValue);
}
