public class HealthBarDirectDisplayer : HealthBarDisplayer
{
    protected override void OnHealthChanged(int oldValue, int newValue)
    {
        Slider.value = (float)newValue / Health.MaxAmount;
        BarImage.enabled = !Health.IsEmpty;
    }
}
