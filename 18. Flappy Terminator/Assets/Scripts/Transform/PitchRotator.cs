using UnityEngine;

public class PitchRotator : MonoBehaviour
{
    [SerializeField] private float _minAngle = -45.0f;
    [SerializeField] private float _maxAngle = 45.0f;
    [SerializeField] private float _minVerticalSpeed = -5.0f;
    [SerializeField] private float _maxVerticalSpeed = 5.0f;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float _verticalSpeedRange;
    
    private void Awake()
    {
        _verticalSpeedRange = Mathf.Abs(_maxVerticalSpeed - _minVerticalSpeed);
    }
    
    private void Update()
    {
        if (_rigidbody.bodyType == RigidbodyType2D.Static)
            return;
        
        float verticalSpeed = Mathf.Clamp(_rigidbody.velocity.y, _minVerticalSpeed, _maxVerticalSpeed);
        float speedFactor = (verticalSpeed - _minVerticalSpeed) / _verticalSpeedRange;
        float pitchAngle = Mathf.Lerp(_minAngle, _maxAngle, speedFactor);
        
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, pitchAngle);
    }
}
