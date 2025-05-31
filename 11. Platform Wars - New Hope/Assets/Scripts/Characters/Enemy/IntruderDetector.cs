using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class IntruderDetector : MonoBehaviour
{
    private BoxCollider2D _area;
    
    public event Action<Transform> Detected;
    public event Action Lost;
    
    private void Awake()
    {
        _area = GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<ChaseTarget>(out _))
            Detected?.Invoke(other.gameObject.transform);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<ChaseTarget>(out _))
            Lost?.Invoke();
    }
    
    public void CalculateAreaBounds(out float leftBorder, out float rightBorder)
    {
        float halfSize = _area.bounds.size.x / 2.0f;
        float centerX = _area.bounds.center.x;
        
        leftBorder = centerX - halfSize;
        rightBorder = centerX + halfSize;
    }
    
    public void DrawBounds()
    {
        Gizmos.color = new Color(0.7f, 0.7f, 0.0f);
        
        BoxCollider2D area = _area;
        
        if (Application.isPlaying == false)
            area = GetComponent<BoxCollider2D>();
        
        Gizmos.DrawWireCube(area.bounds.center, area.bounds.size);
    }
}
