using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JumpAnimationController : MonoBehaviour
{
    [SerializeField] private Jumper _jumper;
    [SerializeField] private GroundDetector _groundDetector;
    
    private Animator _animator;
    
    private bool _isJumping;
    private bool _isGroundLost;
    
    private int _jumpedKeyHash;
    private int _flyingDownKeyHash;
    private int _hitGroundKeyHash;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _jumpedKeyHash = Animator.StringToHash("Jumped");
        _flyingDownKeyHash = Animator.StringToHash("FlyingDown");
        _hitGroundKeyHash = Animator.StringToHash("HitGround");
    }

    private void OnEnable()
    {
        _jumper.Jumped += OnJumped;
        _jumper.FallStarted += OnFallStarted;
        _groundDetector.Lost += OnGroundLost;
        _groundDetector.Stepped += OnGroundStepped;
    }

    private void OnDisable()
    {
        _jumper.Jumped -= OnJumped;
        _jumper.FallStarted -= OnFallStarted;
        _groundDetector.Lost -= OnGroundLost;
        _groundDetector.Stepped -= OnGroundStepped;
    }
    
    private void OnGroundLost()
    {
        _animator.ResetTrigger(_hitGroundKeyHash);
        _isGroundLost = true;
        
        if (_isJumping == false)
            _animator.SetTrigger(_flyingDownKeyHash);
    }

    private void OnGroundStepped()
    {
        if (_isGroundLost == false)
            return;
        
        _animator.SetTrigger(_hitGroundKeyHash);
        _isJumping = false;
        _isGroundLost = false;
    }
    
    private void OnJumped()
    {
        _animator.ResetTrigger(_flyingDownKeyHash);
        _animator.ResetTrigger(_hitGroundKeyHash);
        
        _isJumping = true;
        _animator.SetTrigger(_jumpedKeyHash);
    }

    private void OnFallStarted()
    {
        _animator.ResetTrigger(_hitGroundKeyHash);
        _animator.SetTrigger(_flyingDownKeyHash);
    }
}
