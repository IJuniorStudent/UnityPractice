using UnityEngine;

public abstract class CollectableItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out CollectableInteractor interactor))
            interactor.TryCollect(this);
    }
    
    public abstract bool TryAffect(CollectableInteractor other);
}
