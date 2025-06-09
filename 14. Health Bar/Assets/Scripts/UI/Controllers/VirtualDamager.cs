public class VirtualDamager : VirtualNpc
{
    protected override void OnButtonClicked()
    {
        Health.Decrease(HealthChangeAmount);
    }
}
