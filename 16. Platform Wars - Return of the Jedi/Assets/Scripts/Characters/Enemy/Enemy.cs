using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BotMover))]
[RequireComponent(typeof(AutoPatroller))]
[RequireComponent(typeof(TargetChaser))]
[RequireComponent(typeof(AttackScheduler))]
[RequireComponent(typeof(EventGate))]
[RequireComponent(typeof(Restarter))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _damage = 15;
    [SerializeField] private float _targetAttackDistance = 2.0f;
    [SerializeField] private Canvas _uiElements;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private DamageArea _damageArea;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private DamageAnimationEventReceiver _animationEventReceiver;
    [SerializeField] private IntruderDetector _intruderDetector;
    
    private Health _health;
    private BotMover _botMover;
    private EventGate _eventGate;
    private Restarter _restarter;
    
    private AutoPatroller _autoPatroller;
    private TargetChaser _chaser;
    private AttackScheduler _attacker;
    
    private bool _hasIntruder;
    private Transform _intruderTransform;
    
    public bool IsDead => _health.IsEmpty;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _botMover = GetComponent<BotMover>();
        _autoPatroller = GetComponent<AutoPatroller>();
        _chaser = GetComponent<TargetChaser>();
        _attacker = GetComponent<AttackScheduler>();
        _eventGate = GetComponent<EventGate>();
        _restarter = GetComponent<Restarter>();
        
        _autoPatroller.StartTask();
    }
    
    private void Start()
    {
        _intruderDetector.CalculateAreaBounds(out float leftBorder, out float rightBorder);
        _chaser.Initialize(leftBorder, rightBorder, _targetAttackDistance);
    }
    
    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
        
        _botMover.MoveStarted += OnMoveStarted;
        _botMover.MoveFinished += OnMoveFinished;
        
        _intruderDetector.Detected += OnIntruderDetected;
        _intruderDetector.Lost += OnIntruderLost;
        
        _chaser.Finished += OnChaseFinished;
        _chaser.MoveTargetRequested += OnChaseTargetRequested;
        
        _attacker.Started += OnAttackStarted;
        _attacker.Finished += OnAttackFinished;
        
        _animationEventReceiver.DamageAreaActivated += OnDamageAreaActivated;
        
        _damageArea.DamageDealt += OnDamageDealt;

        _eventGate.PlayerDied += OnIntruderLost;
        _eventGate.Restarted += OnRestarted;
    }
    
    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
        
        _botMover.MoveStarted -= OnMoveStarted;
        _botMover.MoveFinished -= OnMoveFinished;
        
        _intruderDetector.Detected -= OnIntruderDetected;
        _intruderDetector.Lost -= OnIntruderLost;
        
        _chaser.Finished -= OnChaseFinished;
        _chaser.MoveTargetRequested -= OnChaseTargetRequested;

        _attacker.Started -= OnAttackStarted;
        _attacker.Finished -= OnAttackFinished;
        
        _animationEventReceiver.DamageAreaActivated -= OnDamageAreaActivated;
        
        _damageArea.DamageDealt -= OnDamageDealt;
        
        _eventGate.PlayerDied -= OnIntruderLost;
        _eventGate.Restarted -= OnRestarted;
    }
    
    private void OnDrawGizmosSelected()
    {
        _intruderDetector?.DrawBounds();
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
        
        if (newValue - oldValue < 0)
            _animator.Hurt();
    }
    
    private void OnMoveStarted()
    {
        _animator.StartMove();
    }
    
    private void OnMoveFinished()
    {
        _animator.StopMove();
    }
    
    private void OnIntruderDetected(Transform target)
    {
        if (_health.IsEmpty)
            return;
        
        _hasIntruder = true;
        
        _intruderTransform = target;
        _autoPatroller.StopTask();
        _chaser.StartTask(target);
    }
    
    private void OnIntruderLost()
    {
        if (_hasIntruder == false)
            return;
        
        _hasIntruder = false;
        
        if (_health.IsEmpty)
            return;
        
        _botMover.StopMove();
        _chaser.StopTask();
        _autoPatroller.StartTask();
    }
    
    private void OnChaseFinished()
    {
        _botMover.StopMove();
        _chaser.StopTask();
        
        SelectNextTask();
    }
    
    private void OnChaseTargetRequested(Vector3 targetPosition)
    {
        _botMover.MoveTo(targetPosition.x);
    }
    
    private void OnAttackStarted()
    {
        if (IsLookingOnTarget() == false)
            _rotator.ToggleRotation();
        
        _animator.Attack();
    }
    
    private void OnAttackFinished()
    {
        SelectNextTask();
    }
    
    private void SelectNextTask()
    {
        if (_health.IsEmpty || _hasIntruder == false)
            return;
        
        if (IsIntruderInAttackDistance())
            _attacker.StartTask();
        else
            _chaser.StartTask(_intruderTransform);
    }
    
    private bool IsIntruderInAttackDistance()
    {
        float squaredDistance = _targetAttackDistance * _targetAttackDistance;
        
        return (_intruderTransform.position - gameObject.transform.position).sqrMagnitude <= squaredDistance;
    }
    
    private bool IsLookingOnTarget()
    {
        float selfViewDirection = _rotator.gameObject.transform.forward.z;
        float targetDirection = _intruderTransform.position.x - gameObject.transform.position.x;
        
        return selfViewDirection * targetDirection >= 0;
    }
    
    private void OnDamageAreaActivated()
    {
        _damageArea.Trigger();
    }
    
    private void OnDamageDealt(IDamageable target)
    {
        target.TakeDamage(_damage);
    }

    private void StopAllTasks()
    {
        _botMover.StopMove();
        _autoPatroller.StopTask();
        _chaser.StopTask();
        _attacker.StopTask();
    }
    
    private void Die()
    {
        StopAllTasks();
        
        _animator.Die();
        _uiElements.enabled = false;
    }
    
    private void OnRestarted()
    {
        StopAllTasks();
        
        _autoPatroller.StartTask();
        _restarter.Restore();
        _animator.Resurrect();
        _uiElements.enabled = true;
    }
}
