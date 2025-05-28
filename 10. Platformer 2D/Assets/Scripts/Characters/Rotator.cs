using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float FlipRotationAngle = 180.0f;
    
    private Quaternion _lookForwardRotation;
    private Quaternion _lookBackwardRotation;
    
    private void Awake()
    {
        _lookForwardRotation = Quaternion.Euler(0, 0, 0);
        _lookBackwardRotation = Quaternion.Euler(0, FlipRotationAngle, 0);
    }
    
    public void SetLookDirection(bool isForward)
    {
        gameObject.transform.rotation = isForward ? _lookForwardRotation : _lookBackwardRotation;
    }
}
