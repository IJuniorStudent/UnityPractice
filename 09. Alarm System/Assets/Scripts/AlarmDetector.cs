using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AlarmDetector : MonoBehaviour
{
    public event Action ActivityDetected;
    public event Action ActivityLost;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<AlarmTarget>(out _))
            ActivityDetected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<AlarmTarget>(out _))
            ActivityLost?.Invoke();
    }
}
