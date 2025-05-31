using System;
using UnityEngine;

public class CollectableInteractor : MonoBehaviour
{
    public event Action<CollectableItem> Collected;
    public event Action<CollectableItem> CollectConfirmed;
    
    public void TryCollect(CollectableItem item)
    {
        Collected?.Invoke(item);
    }
    
    public void ConfirmCollected(CollectableItem item)
    {
        CollectConfirmed?.Invoke(item);
    }
}
