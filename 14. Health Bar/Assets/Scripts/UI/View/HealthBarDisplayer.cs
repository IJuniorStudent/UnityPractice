using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthBarDisplayer : HealthDisplayer
{
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
}
