using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explodePower = 1000.0f;
    
    public void MakeShockWave(Vector3 center, Rigidbody target)
    {
        Vector3 targetPosition = target.gameObject.transform.position;
        Vector3 forceVector = targetPosition + (targetPosition - center).normalized * _explodePower;
        
        target.AddForce(forceVector);
    }
}
