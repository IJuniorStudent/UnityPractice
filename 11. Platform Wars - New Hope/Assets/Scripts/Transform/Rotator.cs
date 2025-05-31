using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float FlipRotateAngle = 180.0f;
    
    private Quaternion _lookForwardDirection;
    private Quaternion _lookBackwardDirection;

    private bool _isLookingForward;
    
    private void Awake()
    {
        _lookForwardDirection = Quaternion.Euler(0, 0, 0);
        _lookBackwardDirection = Quaternion.Euler(0, FlipRotateAngle, 0);
    }
    
    public void SetDirection(bool isForward)
    {
        _isLookingForward = isForward;
        
        gameObject.transform.rotation = isForward ? _lookForwardDirection : _lookBackwardDirection;
    }
    
    public void ToggleRotation()
    {
        gameObject.transform.rotation = _isLookingForward ? _lookBackwardDirection : _lookForwardDirection;
        _isLookingForward = !_isLookingForward;
    }
}
