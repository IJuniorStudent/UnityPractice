using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ResourceStorage))]
public class ResourceStorageView : MonoBehaviour
{
    [SerializeField] private Text _resourcesAmount;
    
    private ResourceStorage _storage;
    
    private void Awake()
    {
        _storage = GetComponent<ResourceStorage>();
    }
    
    private void OnEnable()
    {
        _storage.AmountChanged += OnResourceAmountChanged;
    }
    
    private void OnDisable()
    {
        _storage.AmountChanged -= OnResourceAmountChanged;
    }
    
    private void OnResourceAmountChanged(int amount)
    {
        _resourcesAmount.text = amount.ToString();
    }
}
