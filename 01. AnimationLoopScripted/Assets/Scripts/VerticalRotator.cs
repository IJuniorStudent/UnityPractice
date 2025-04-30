using UnityEngine;

public class VerticalRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 100.0f;
    
    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}
