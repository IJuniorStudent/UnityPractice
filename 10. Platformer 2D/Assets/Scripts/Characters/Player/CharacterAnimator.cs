using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _isMovingKeyHash;
    private int _jumpedKeyHash;
    private int _flyingDownKeyHash;
    private int _hitGroundKeyHash;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _isMovingKeyHash = Animator.StringToHash("IsMoving");
        _jumpedKeyHash = Animator.StringToHash("Jumped");
        _flyingDownKeyHash = Animator.StringToHash("FlyingDown");
        _hitGroundKeyHash = Animator.StringToHash("HitGround");
    }
    
    public void StartMove()
    {
        _animator.SetBool(_isMovingKeyHash, true);
    }
    
    public void StopMove()
    {
        _animator.SetBool(_isMovingKeyHash, false);
    }
    
    public void Jump()
    {
        _animator.ResetTrigger(_flyingDownKeyHash);
        _animator.ResetTrigger(_hitGroundKeyHash);
        _animator.SetTrigger(_jumpedKeyHash);
    }
    
    public void StartFall()
    {
        _animator.ResetTrigger(_hitGroundKeyHash);
        _animator.ResetTrigger(_jumpedKeyHash);
        _animator.SetTrigger(_flyingDownKeyHash);
    }
    
    public void StopFall()
    {
        _animator.SetTrigger(_hitGroundKeyHash);
    }
}
