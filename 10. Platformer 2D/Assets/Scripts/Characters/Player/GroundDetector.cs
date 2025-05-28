using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private const int DetectTouchCount = 1;
    
    private int _touchCount = 0;
    
    public event Action Lost;
    public event Action Stepped;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _) == false)
            return;
        
        _touchCount++;
        
        if (_touchCount == DetectTouchCount)
            Stepped?.Invoke();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _) == false)
            return;
        
        _touchCount--;
        
        if (_touchCount < DetectTouchCount)
            Lost?.Invoke();
    }
}
