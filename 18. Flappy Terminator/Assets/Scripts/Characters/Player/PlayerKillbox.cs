using UnityEngine;

public class PlayerKillbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            player.Explode();
    }
}
