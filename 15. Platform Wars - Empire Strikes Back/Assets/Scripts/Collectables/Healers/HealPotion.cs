using UnityEngine;

public class HealPotion : CollectableItem
{
    [SerializeField] private int _points = 20;
    
    public int Points => _points;
}
