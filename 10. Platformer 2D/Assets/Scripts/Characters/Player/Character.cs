using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Character : MonoBehaviour
{
    private const float FallingVelocityThreshold = 0.01f;
    
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private CharacterAnimator _characterAnimator;
    
    private InputReceiver _receiver;
    
    private bool _isJumping;
    private bool _isFalling;
    
    private void Awake()
    {
        _receiver = GetComponent<InputReceiver>();
    }
    
    private void OnEnable()
    {
        _receiver.MoveButtonDown += OnMovePressed;
        _receiver.MoveButtonUp += OnMoveReleased;
        _receiver.JumpButtonDown += OnJumpPressed;
        
        _groundDetector.Lost += OnGroundLost;
        _groundDetector.Stepped += OnGroundStepped;
    }
    
    private void OnDisable()
    {
        _receiver.MoveButtonDown -= OnMovePressed;
        _receiver.MoveButtonUp -= OnMoveReleased;
        _receiver.JumpButtonDown -= OnJumpPressed;
        
        _groundDetector.Lost -= OnGroundLost;
        _groundDetector.Stepped -= OnGroundStepped;
    }
    
    private void Update()
    {
        if (_isJumping == false || _isFalling || _mover.VerticalSpeed > -FallingVelocityThreshold)
            return;
        
        _isFalling = true;
        _characterAnimator.StartFall();
    }
    
    private void OnMovePressed(float axisDelta)
    {
        _mover.StartMove(axisDelta);
        
        bool isMoveForward = axisDelta > 0.0f;
        _characterAnimator.StartMove(isMoveForward);
    }
    
    private void OnMoveReleased()
    {
        _mover.StopMove();
        _characterAnimator.StopMove();
    }
    
    private void OnJumpPressed(float axisDelta)
    {
        if (_isJumping || _isFalling)
            return;
        
        _isJumping = true;
        _jumper.Jump();
        _characterAnimator.Jump();
    }
    
    private void OnGroundLost()
    {
        if (_isJumping)
            return;

        _isFalling = true;
        _characterAnimator.StartFall();
    }
    
    private void OnGroundStepped()
    {
        _isJumping = false;
        _isFalling = false;
        _characterAnimator.StopFall();
    }
}
