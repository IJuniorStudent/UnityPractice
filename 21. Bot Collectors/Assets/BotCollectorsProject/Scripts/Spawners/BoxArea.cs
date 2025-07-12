using UnityEngine;

public class BoxArea : MonoBehaviour
{
    private Vector3 _minPoint;
    private Vector3 _maxPoint;
    
    private void Awake()
    {
        Vector3 halfScale = gameObject.transform.localScale / 2.0f;
        
        _minPoint = gameObject.transform.position - halfScale;
        _maxPoint = gameObject.transform.position + halfScale;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, gameObject.transform.localScale);
    }
    
    public Vector3 GetRandomPoint()
    {
        return new Vector3(
            Random.Range(_minPoint.x, _maxPoint.x),
            Random.Range(_minPoint.y, _maxPoint.y),
            Random.Range(_minPoint.z, _maxPoint.z)
        );
    }
}
