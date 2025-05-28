using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MonsterAnimator : MonoBehaviour
{
    private const float FlipRotationAngle = 180.0f;
    
    private Animator _animator;
    private int _isMovingKeyHash;
    
    private Quaternion _lookForwardRotation;
    private Quaternion _lookBackwardRotation;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isMovingKeyHash = Animator.StringToHash("IsMoving");
        
        _lookForwardRotation = Quaternion.Euler(0, 0, 0);
        _lookBackwardRotation = Quaternion.Euler(0, FlipRotationAngle, 0);
    }
    
    public void StartMove(bool isForward)
    {
        gameObject.transform.rotation = isForward ? _lookForwardRotation : _lookBackwardRotation;
        _animator.SetBool(_isMovingKeyHash, true);
    }
    
    public void StopMove()
    {
        _animator.SetBool(_isMovingKeyHash, false);
    }
}
