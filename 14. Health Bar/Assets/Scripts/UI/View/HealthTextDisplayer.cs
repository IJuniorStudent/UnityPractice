using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthTextDisplayer : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        
        DisplayHealth(_health.Amount);
    }
    
    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
    }
    
    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }
    
    private void OnHealthChanged(int oldValue, int newValue)
    {
        DisplayHealth(newValue);
    }
    
    private void DisplayHealth(int value)
    {
        _text.text = $"{value} / {_health.MaxAmount}";
    }
}
