using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthBarDisplayer : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected Image BarImage;
    
    protected Slider Slider;
    
    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }
    
    private void Start()
    {
        Slider.value = Health.Amount / Health.MaxAmount;
    }
    
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
