using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    
    private int _isMovingHash;
    private int _jumpedHash;
    private int _reachedHighJumpPosHash;
    private int _fallingHash;
    private int _hitGroundHash;
    private int _damagedHash;
    private int _attackedHash;
    private int _comboAttackedHash;
    private int _diedHash;
    private int _resurrectedHash;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InitAnimatorParameters();
    }
    
    public void StartMove()
    {
        _animator.SetBool(_isMovingHash, true);
    }

    public void StopMove()
    {
        _animator.SetBool(_isMovingHash, false);
    }
    
    public void Jump()
    {
        _animator.ResetTrigger(_hitGroundHash);
        _animator.ResetTrigger(_reachedHighJumpPosHash);
        _animator.ResetTrigger(_fallingHash);
        _animator.SetTrigger(_jumpedHash);
    }
    
    public void FallFromJump()
    {
        _animator.ResetTrigger(_jumpedHash);
        _animator.SetTrigger(_reachedHighJumpPosHash);
    }
    
    public void StartFall()
    {
        _animator.ResetTrigger(_hitGroundHash);
        _animator.ResetTrigger(_reachedHighJumpPosHash);
        _animator.SetTrigger(_fallingHash);
    }

    public void StopFall()
    {
        _animator.ResetTrigger(_fallingHash);
        _animator.SetTrigger(_hitGroundHash);
    }
    
    public void Attack()
    {
        _animator.ResetTrigger(_comboAttackedHash);
        _animator.SetTrigger(_attackedHash);
    }
    
    public void ComboAttack()
    {
        _animator.ResetTrigger(_attackedHash);
        _animator.SetTrigger(_comboAttackedHash);
    }
    
    public void Hurt()
    {
        _animator.ResetTrigger(_damagedHash);
        _animator.SetTrigger(_damagedHash);
    }
    
    public void Die()
    {
        _animator.ResetTrigger(_resurrectedHash);
        _animator.SetTrigger(_diedHash);
    }
    
    public void Resurrect()
    {
        _animator.ResetTrigger(_diedHash);
        _animator.SetTrigger(_resurrectedHash);
    }
    
    private void InitAnimatorParameters()
    {
        _isMovingHash = Animator.StringToHash("IsMoving");
        _jumpedHash = Animator.StringToHash("Jumped");
        _reachedHighJumpPosHash = Animator.StringToHash("ReachedHighJumpPos");
        _fallingHash = Animator.StringToHash("Falling");
        _hitGroundHash = Animator.StringToHash("HitGround");
        _damagedHash = Animator.StringToHash("Damaged");
        _attackedHash = Animator.StringToHash("Attacked");
        _comboAttackedHash = Animator.StringToHash("ComboAttacked");
        _diedHash = Animator.StringToHash("Died");
        _resurrectedHash = Animator.StringToHash("Resurrected");
    }
}
