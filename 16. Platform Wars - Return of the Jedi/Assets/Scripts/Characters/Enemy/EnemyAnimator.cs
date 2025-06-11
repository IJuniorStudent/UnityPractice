using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    
    private int _isMovingHash;
    private int _attackedHash;
    private int _damagedHash;
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
    
    public void Attack()
    {
        _animator.ResetTrigger(_attackedHash);
        _animator.SetTrigger(_attackedHash);
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
        _damagedHash = Animator.StringToHash("Damaged");
        _attackedHash = Animator.StringToHash("Attacked");
        _diedHash = Animator.StringToHash("Died");
        _resurrectedHash = Animator.StringToHash("Resurrected");
    }
}
