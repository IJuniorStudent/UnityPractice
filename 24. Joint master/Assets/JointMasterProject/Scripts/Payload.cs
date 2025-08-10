using UnityEngine;

public class Payload : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _) == false)
            return;
        
        float destroyDelay = 2.0f;
        
        Destroy(gameObject, destroyDelay);
    }
}
