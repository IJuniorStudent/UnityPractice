using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explodePower = 1000.0f;
    
    public void MakeShockWave(Vector3 center, Rigidbody target)
    {
        Vector3 forceVector = CalculateForce(center, target.gameObject.transform.position);
        
        target.AddForce(forceVector);
    }
    
    public void MakeShockWaveIndirect(Vector3 center, float maxDistance, float forceMultiplier, Rigidbody target)
    {
        Vector3 targetPosition = target.gameObject.transform.position;
        
        float distance = (targetPosition - center).magnitude;
        float distanceMultiplier = Mathf.Sqrt(distance / maxDistance);
        
        Vector3 forceVector = forceMultiplier * distanceMultiplier * CalculateForce(center, targetPosition);
        
        target.AddForce(forceVector);
    }

    private Vector3 CalculateForce(Vector3 sourcePosition, Vector3 targetPosition)
    {
        return targetPosition + (targetPosition - sourcePosition).normalized * _explodePower;
    }
}
