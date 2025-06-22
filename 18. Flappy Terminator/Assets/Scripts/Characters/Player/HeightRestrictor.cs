using UnityEngine;

public class HeightRestrictor : MonoBehaviour
{
    private float _floorPosition;
    private float _ceilPosition;
    private float _areaHeight;
    
    private void Awake()
    {
        Vector3 halfScale = transform.localScale / 2.0f;
        
        _floorPosition = transform.position.y - halfScale.y;
        _ceilPosition = transform.position.y + halfScale.y;
        _areaHeight = _ceilPosition - _floorPosition;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    
    public float GetCeilDistancePercentage(float verticalPosition)
    {
        float height = Mathf.Clamp(verticalPosition, _floorPosition, _ceilPosition);
        
        return 1.0f - ((height - _floorPosition) / _areaHeight);
    }
}
