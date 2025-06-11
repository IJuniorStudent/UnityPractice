using UnityEngine;

[RequireComponent(typeof(Health))]
public class Restarter : MonoBehaviour
{
    private Health _health;
    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        
        _initialPosition = gameObject.transform.position;
        _initialRotation = gameObject.transform.rotation;
    }
    
    public void Restore()
    {
        _health.Reset();
        
        gameObject.transform.position = _initialPosition;
        gameObject.transform.rotation = _initialRotation;
    }
}
