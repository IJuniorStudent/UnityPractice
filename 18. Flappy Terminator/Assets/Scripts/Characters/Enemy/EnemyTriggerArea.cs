using UnityEngine;

public abstract class EnemyTriggerArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            OnEntered(enemy);
    }
    
    protected abstract void OnEntered(Enemy enemy);
}
