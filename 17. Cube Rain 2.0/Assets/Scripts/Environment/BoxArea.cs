using UnityEngine;

public class BoxArea : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
    }
    
    public Vector3 GetRandomPosition()
    {
        Vector3 center = gameObject.transform.position;
        Vector3 halfScale = gameObject.transform.localScale / 2.0f;
        
        Vector3 minPosition = center - halfScale;
        Vector3 maxPosition = center + halfScale;
        
        return new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );
    }
}
