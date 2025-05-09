using UnityEngine;

public class ForwardMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    
    private void Update()
    {
        transform.Translate(_moveSpeed * Time.deltaTime * transform.forward, Space.World);
    }
}
