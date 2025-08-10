using UnityEngine;

public class SwingPusher : MonoBehaviour
{
    [SerializeField] private Vector3 _force = Vector3.forward;
    [SerializeField] private Rigidbody _rigidbody;
    
    public void Push()
    {
        _rigidbody.AddForce(_force, ForceMode.Impulse);
    }
}
