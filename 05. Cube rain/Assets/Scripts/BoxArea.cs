using UnityEditor;
using UnityEngine;

public class BoxArea : MonoBehaviour
{
    [SerializeField] private Vector3 _center = new (0, 0, 0);
    [SerializeField] private Vector3 _size = new (10, 10, 10);
    [SerializeField] private bool _isDrawAlways = false;
    
    public Vector3 GetRandomPoint()
    {
        Vector3 halfSize = _size / 2.0f;
        
        Vector3 minPoint = _center - halfSize;
        Vector3 maxPoint = _center + halfSize;
        
        float randX = Random.Range(minPoint.x, maxPoint.x);
        float randY = Random.Range(minPoint.y, maxPoint.y);
        float randZ = Random.Range(minPoint.z, maxPoint.z);
        
        return new Vector3(randX, randY, randZ);
    }
    
    private void OnDrawGizmos()
    {
        if (_isDrawAlways == false && Selection.Contains(gameObject) == false)
            return;
        
        Gizmos.DrawWireCube(_center, _size);
    }
}
