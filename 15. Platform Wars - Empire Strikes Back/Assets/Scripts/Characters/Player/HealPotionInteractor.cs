using UnityEngine;

public class HealPotionInteractor : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    public bool TryCollect(HealPotion potion)
    {
        if (_health.IsFull)
            return false;
        
        _health.Increase(potion.Points);
        return true;
    }
}
