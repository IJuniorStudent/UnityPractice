public class VirtualHealer : VirtualNpc
{
    protected override void OnButtonClicked()
    {
        Health.Increase(HealthChangeAmount);
    }
}
