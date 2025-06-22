using UnityEngine;

public class BackgroundRepeatTarget : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out BaseImageMover imageMover))
            imageMover.ResetState();
    }
}
