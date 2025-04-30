using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 100.0f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}
