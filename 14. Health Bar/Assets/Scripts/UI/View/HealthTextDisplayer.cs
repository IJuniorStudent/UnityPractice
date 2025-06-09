using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthTextDisplayer : HealthDisplayer
{
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        
        DisplayHealth(Health.Amount);
    }
    
    protected override void OnHealthChanged(int oldValue, int newValue)
    {
        DisplayHealth(newValue);
    }
    
    private void DisplayHealth(int value)
    {
        _text.text = $"{value} / {Health.MaxAmount}";
    }
}
