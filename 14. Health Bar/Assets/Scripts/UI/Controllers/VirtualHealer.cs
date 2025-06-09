using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VirtualHealer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _healAmount = 15;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnHealClicked);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnHealClicked);
    }
    
    private void OnHealClicked()
    {
        _health.Increase(_healAmount);
    }
}
