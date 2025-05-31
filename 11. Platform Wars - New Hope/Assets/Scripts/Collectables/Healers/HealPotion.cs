using UnityEngine;

public class HealPotion : CollectableItem
{
    [SerializeField] private int _points = 20;
    
    public override bool TryAffect(CollectableInteractor other)
    {
        if (other.gameObject.TryGetComponent(out Health health) == false)
            return false;
        
        if (health.IsFull)
            return false;
        
        health.Increase(_points);
        return true;
    }
}
