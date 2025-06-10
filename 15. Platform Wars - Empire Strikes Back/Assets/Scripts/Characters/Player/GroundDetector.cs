using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private const int DetectionHitCount = 1;
    
    private int _hitCount = 0;
    
    public event Action Lost;
    public event Action Stepped;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _) == false)
            return;
        
        _hitCount++;
        
        if (_hitCount == DetectionHitCount)
            Stepped?.Invoke();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _) == false)
            return;
        
        _hitCount--;
        
        if (_hitCount < DetectionHitCount)
            Lost?.Invoke();
    }
}
