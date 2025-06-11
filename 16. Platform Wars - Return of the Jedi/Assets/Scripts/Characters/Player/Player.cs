using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(EventGate))]
[RequireComponent(typeof(Restarter))]
[RequireComponent(typeof(HealthAbsorber))]
public class Player : MonoBehaviour, IDamageable
{
    private const float VelocityThreshold = 0.5f;
    
    [SerializeField] private InputReceiver _inputReceiver;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private ComboAnimationEventReceiver _comboAnimEventReceiver;
    [SerializeField] private DamageAnimationEventReceiver _damageAnimEventReceiver;
    [SerializeField] private DamageArea _damageArea;
    [SerializeField] private int _attackDamage = 15;
    [SerializeField] private int _runAttackDamage = 20;
    [SerializeField] private int _comboAttackDamage = 20;
    
    private Health _health;
    private Mover _mover;
    private Jumper _jumper;
    private EventGate _eventGate;
    private Restarter _restarter;
    private HealthAbsorber _absorber;
    
    private bool _isJumping;
    private bool _isGrounded;
    private bool _isFalling;
    private bool _isAttacking;
    private bool _canComboAttack;
    private bool _isPerformCombo;
    private bool _isComboAttack;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _eventGate = GetComponent<EventGate>();
        _restarter = GetComponent<Restarter>();
        _absorber = GetComponent<HealthAbsorber>();
    }
    
    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
        
        _inputReceiver.MovePressed += OnMovePressed;
        _inputReceiver.MoveReleased += OnMoveReleased;
        _inputReceiver.JumpPressed += OnJumpPressed;
        _inputReceiver.FirePressed += OnFirePressed;
        _inputReceiver.AltFirePressed += OnAltFirePressed;
        
        _groundDetector.Lost += OnGroundLost;
        _groundDetector.Stepped += OnGroundStepped;
        
        _comboAnimEventReceiver.ComboWindowOpened += OnComboWindowOpened;
        _comboAnimEventReceiver.ComboWindowClosed += OnComboWindowClosed;
        _comboAnimEventReceiver.ComboCanBePerformed += OnComboContinue;
        
        _damageAnimEventReceiver.DamageAreaActivated += OnDamageAreaActivated;
        
        _damageArea.DamageDealt += OnDamageDealt;
        
        _eventGate.Restarted += OnRestarted;
    }
    
    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
        
        _inputReceiver.MovePressed -= OnMovePressed;
        _inputReceiver.MoveReleased -= OnMoveReleased;
        _inputReceiver.JumpPressed -= OnJumpPressed;
        _inputReceiver.FirePressed -= OnFirePressed;
        _inputReceiver.AltFirePressed -= OnAltFirePressed;
        
        _groundDetector.Lost -= OnGroundLost;
        _groundDetector.Stepped -= OnGroundStepped;
        
        _comboAnimEventReceiver.ComboWindowOpened -= OnComboWindowOpened;
        _comboAnimEventReceiver.ComboWindowClosed -= OnComboWindowClosed;
        _comboAnimEventReceiver.ComboCanBePerformed -= OnComboContinue;
        
        _damageAnimEventReceiver.DamageAreaActivated -= OnDamageAreaActivated;
        
        _damageArea.DamageDealt -= OnDamageDealt;
        
        _eventGate.Restarted -= OnRestarted;
    }
    
    private void FixedUpdate()
    {
        if (_isJumping == false || _isFalling || _mover.VerticalSpeed > -VelocityThreshold)
            return;
        
        _isFalling = true;
        _animator.FallFromJump();
    }
    
    public void TakeDamage(int damage)
    {
        if (_health.IsEmpty)
            return;
        
        _health.Decrease(damage);
    }
    
    private void OnHealthChanged(int oldValue, int newValue)
    {
        if (newValue == 0)
        {
            Die();
            return;
        }
        
        if (newValue - oldValue < 0 && _isAttacking == false)
            _animator.Hurt();
    }
    
    private void OnMovePressed(float axisValue)
    {
        if (_health.IsEmpty)
            return;
        
        bool isLookForward = axisValue > 0;
        
        _rotator.SetDirection(isLookForward);
        _animator.StartMove();
        _mover.StartMove(axisValue);
    }
    
    private void OnMoveReleased()
    {
        if (_health.IsEmpty)
            return;
        
        _animator.StopMove();
        _mover.StopMove();
    }
    
    private void OnJumpPressed(float axisValue)
    {
        if (_health.IsEmpty)
        {
            _eventGate.Notify(GlobalEvent.Restart);
            return;
        }
        
        if (_isJumping || _isGrounded == false)
            return;
        
        _isJumping = true;
        _isFalling = false;
        
        _jumper.Jump();
        _animator.Jump();
    }
    
    private void OnGroundLost()
    {
        _isGrounded = false;
        
        if (_isJumping == false)
            _animator.StartFall();
    }
    
    private void OnGroundStepped()
    {
        _isGrounded = true;
        _isJumping = false;
        _animator.StopFall();
    }
    
    private void OnFirePressed(float axisValue)
    {
        if (_health.IsEmpty)
            return;
        
        if (_isAttacking && _canComboAttack == false)
            return;
        
        _isAttacking = true;
        
        if (_canComboAttack)
            _isPerformCombo = true;
        else
            _animator.Attack();
    }
    
    private void OnAltFirePressed(float axisValue)
    {
        _absorber.Activate();
    }
    
    private void OnComboWindowOpened()
    {
        _canComboAttack = true;
    }
    
    private void OnComboContinue()
    {
        if (_isPerformCombo == false)
            return;
        
        _isPerformCombo = false;
        _isComboAttack = true;
        _animator.ComboAttack();
    }
    
    private void OnComboWindowClosed()
    {
        _canComboAttack = false;
        _isAttacking = false;
        _isComboAttack = false;
    }
    
    private void OnDamageAreaActivated()
    {
        _damageArea.Trigger();
    }
    
    private void OnDamageDealt(IDamageable target)
    {
        if (_isComboAttack)
            target.TakeDamage(_comboAttackDamage);
        else
            target.TakeDamage(_mover.IsMoving ? _runAttackDamage : _attackDamage);
    }
    
    private void Die()
    {
        _eventGate.Notify(GlobalEvent.PlayerDie);
        _animator.Die();
    }
    
    private void OnRestarted()
    {
        _restarter.Restore();
        _animator.Resurrect();
    }
}
