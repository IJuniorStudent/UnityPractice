using UnityEngine;

public class AbsorbArea : CollectorArea
{
    public bool TryFindClosestAliveTarget(out IDamageable damageable)
    {
        damageable = null;
        
        int collectedCount = Collect();
        
        if (collectedCount == 0)
            return false;
        
        float closestDistanceSquared = float.MaxValue;
        Vector3 position = gameObject.transform.position;
        
        for (int i = 0; i < collectedCount; i++)
        {
            if (Overlapped[i].gameObject.TryGetComponent(out Enemy enemy) == false)
                continue;
            
            if (enemy.IsDead)
                continue;
            
            if (damageable == null)
            {
                damageable = enemy;
                closestDistanceSquared = (enemy.gameObject.transform.position - position).sqrMagnitude;
                continue;
            }
            
            float distanceSquared = (enemy.gameObject.transform.position - position).sqrMagnitude;
            
            if (distanceSquared < closestDistanceSquared)
            {
                damageable = enemy;
                closestDistanceSquared = distanceSquared;
            }
        }
        
        return damageable != null;
    }
    
    protected override Color GetGizmosColor()
    {
        return Color.red;
    }
}
