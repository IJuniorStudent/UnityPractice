using System;
using UnityEngine;

[RequireComponent(typeof(ResourceCollector))]
[RequireComponent(typeof(ResourceTransporter))]
[RequireComponent(typeof(ResourceBaseBuilder))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    
    private StateMachine<Unit> _stateMachine;
    private ResourceCollector _collector;
    private ResourceTransporter _transporter;
    private ResourceBaseBuilder _builder;
    private ResourceBase _homeBase;
    
    public Transform MoveTarget { get; private set; }
    public float MoveSpeed => _moveSpeed;
    
    public event Action<Unit> ResourceDelivered;
    public event Action<Unit> ResourceBaseCreated;
    
    private void Awake()
    {
        _collector = GetComponent<ResourceCollector>();
        _builder = GetComponent<ResourceBaseBuilder>();
        _transporter = GetComponent<ResourceTransporter>();
        
        var stateFactory = new UnitStateFactory(this);
        _stateMachine = new StateMachine<Unit>(stateFactory.CreateStates());
        _stateMachine.ChangeState<UnitIdleState>();
    }
    
    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
        _collector.ResourceDelivered += OnResourceDelivered;
        _builder.ResourceBaseBuilt += OnResourceBaseBuilt;
    }
    
    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
        _collector.ResourceDelivered -= OnResourceDelivered;
        _builder.ResourceBaseBuilt -= OnResourceBaseBuilt;
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }
    
    public void SetHomeBase(ResourceBase homeBase)
    {
        _homeBase = homeBase;
    }
    
    public void CollectResource(CollectableResource resource)
    {
        if (_stateMachine.IsInState<UnitIdleState>() == false)
            return;
        
        _collector.SetCollectTarget(resource);
        MoveTo(resource.gameObject.transform);
    }
    
    public void BuildBase(Transform moveTarget)
    {
        if (_stateMachine.IsInState<UnitIdleState>() == false)
            return;
        
        MoveTarget = moveTarget;
        _stateMachine.ChangeState<UnitBuildMoveState>();
    }
    
    public void UpdateBuildTarget()
    {
        if (_stateMachine.IsInState<UnitBuildMoveState>() == false)
            return;
        
        _stateMachine.ChangeState<UnitIdleState>();
        BuildBase(MoveTarget);
    }
    
    public bool TryDetachResource(out CollectableResource resource)
    {
        return _transporter.TryDetach(out resource);
    }
    
    private void MoveTo(Transform moveTarget)
    {
        if (_stateMachine.IsInState<UnitIdleState>() == false)
            return;
        
        MoveTarget = moveTarget;
        _stateMachine.ChangeState<UnitMoveState>();
    }
    
    private void OnResourceCollected(CollectableResource resource)
    {
        if (_stateMachine.IsInState<UnitMoveState>() == false || _transporter.TryAttach(resource) == false)
            return;
        
        resource.SetStateCollected();
        _stateMachine.ChangeState<UnitIdleState>();
        
        MoveTo(_homeBase.gameObject.transform);
    }
    
    private void OnResourceDelivered(ResourceBase resourceBase)
    {
        if (resourceBase != _homeBase || _transporter.IsAttached == false)
            return;
        
        _stateMachine.ChangeState<UnitIdleState>();
        ResourceDelivered?.Invoke(this);
    }
    
    private void OnResourceBaseBuilt()
    {
        if (_stateMachine.IsInState<UnitBuildMoveState>() == false)
            return;
        
        _stateMachine.ChangeState<UnitIdleState>();
        ResourceBaseCreated?.Invoke(this);
    }
}
