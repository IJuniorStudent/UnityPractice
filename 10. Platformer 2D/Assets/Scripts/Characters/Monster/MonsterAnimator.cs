using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MonsterAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _isMovingKeyHash;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isMovingKeyHash = Animator.StringToHash("IsMoving");
    }
    
    public void StartMove()
    {
        _animator.SetBool(_isMovingKeyHash, true);
    }
    
    public void StopMove()
    {
        _animator.SetBool(_isMovingKeyHash, false);
    }
}
