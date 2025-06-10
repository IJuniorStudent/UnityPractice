using System;
using UnityEngine;

public class CollectableInteractor : MonoBehaviour
{
    [SerializeField] private HealPotionInteractor _healPotionInteractor;
    
    public event Action<CollectableItem> CollectConfirmed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out CollectableItem item) == false)
            return;

        bool isItemUsed = false;
        
        switch (item)
        {
            case HealPotion potion:
                isItemUsed = _healPotionInteractor.TryCollect(potion);
                break;
        }
        
        if (isItemUsed)
            CollectConfirmed?.Invoke(item);
    }
}
