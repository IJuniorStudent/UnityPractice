using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class VirtualNpc : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected int HealthChangeAmount = 15;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }
    
    protected abstract void OnButtonClicked();
}
