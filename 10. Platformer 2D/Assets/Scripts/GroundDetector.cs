using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public event Action Lost;
    public event Action Stepped;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _))
            Stepped?.Invoke();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out _))
            Lost?.Invoke();
    }
}
