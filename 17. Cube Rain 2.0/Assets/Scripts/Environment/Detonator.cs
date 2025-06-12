using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float _radius = 7.0f;
    [SerializeField] private float _force = 1500.0f;
    
    public void Explode(Vector3 position)
    {
        RaycastHit[] objects = Physics.SphereCastAll(position, _radius, Vector3.up);
        
        foreach (var hit in objects)
            hit.rigidbody?.AddExplosionForce(_force, position, _radius);
    }
}
