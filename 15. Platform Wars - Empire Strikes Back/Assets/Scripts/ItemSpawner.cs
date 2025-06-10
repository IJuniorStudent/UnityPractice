using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventGate))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private CollectableInteractor _interactor;
    
    private EventGate _eventGate;
    
    private List<CollectableItem> _despawned = new ();
    
    private void Awake()
    {
        _eventGate = GetComponent<EventGate>();
    }
    
    private void OnEnable()
    {
        _interactor.CollectConfirmed += OnCollectConfirmed;
        _eventGate.Restarted += OnRestarted;
    }
    
    private void OnDisable()
    {
        _interactor.CollectConfirmed -= OnCollectConfirmed;
        _eventGate.Restarted -= OnRestarted;
    }
    
    private void OnCollectConfirmed(CollectableItem collectable)
    {
        collectable.gameObject.SetActive(false);
        _despawned.Add(collectable);
    }
    
    private void OnRestarted()
    {
        foreach (CollectableItem item in _despawned)
            item.gameObject.SetActive(true);
        
        _despawned.Clear();
    }
}
