using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VirtualDamager : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _damage = 15;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnDamageClicked);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnDamageClicked);
    }
    
    private void OnDamageClicked()
    {
        _health.Decrease(_damage);
    }
}
