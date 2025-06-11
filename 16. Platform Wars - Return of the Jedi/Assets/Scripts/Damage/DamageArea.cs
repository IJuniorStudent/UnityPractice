using System;
using UnityEngine;

public class DamageArea : CollectorArea
{
    public event Action<IDamageable> DamageDealt;
    public void Trigger()
    {
        int collectedCount = Collect();
        
        for (int i = 0; i < collectedCount; i++)
        {
            if (Overlapped[i].gameObject.TryGetComponent(out IDamageable damageable))
                DamageDealt?.Invoke(damageable);
        }
    }
    
    protected override Color GetGizmosColor()
    {
        return Color.green;
    }
}
