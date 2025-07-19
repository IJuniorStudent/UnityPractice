using System;
using UnityEngine;

[RequireComponent(typeof(ResourceCollector))]
[RequireComponent(typeof(ResourceBaseBuilder))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    
    private StateMachine<Unit> _stateMachine;
    private ResourceCollector _collector;
    private ResourceBaseBuilder _builder;
    
    public Transform MoveTarget { get; private set; }
    public float MoveSpeed => _moveSpeed;
    
    public event Action<Unit, CollectableResource> ResourceCollected;
    public event Action<Unit, CollectableResource> ResourceStored;
    public event Action<Unit> ResourceBaseCreated;
    
    private void Awake()
    {
        _collector = GetComponent<ResourceCollector>();
        _builder = GetComponent<ResourceBaseBuilder>();
        
        var stateFactory = new UnitStateFactory(this);
        _stateMachine = new StateMachine<Unit>(stateFactory.CreateStates());
        _stateMachine.ChangeState<UnitIdleState>();
    }
    
    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
        _collector.ResourceDelivered += OnResourceStored;
        _builder.ResourceBaseBuilt += OnResourceBaseBuilt;
    }
    
    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
        _collector.ResourceDelivered -= OnResourceStored;
        _builder.ResourceBaseBuilt -= OnResourceBaseBuilt;
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }
    
    public void SetHomeStorage(ResourceStorage storage)
    {
        _collector.SetHomeStorage(storage);
    }
    
    public void CollectResource(CollectableResource resource)
    {
        if (_stateMachine.IsInState<UnitIdleState>() == false)
            return;
        
        _collector.SetCollectTarget(resource);
        MoveTo(resource.gameObject.transform);
    }
    
    public void MoveTo(Transform moveTarget)
    {
        if (_stateMachine.IsInState<UnitIdleState>() == false)
            return;
        
        MoveTarget = moveTarget;
        _stateMachine.ChangeState<UnitMoveState>();
    }
    
    public void UpdateMoveTarget(Transform moveTarget)
    {
        if (_stateMachine.IsInState<UnitMoveState>() == false)
            return;
        
        _stateMachine.ChangeState<UnitIdleState>();
        MoveTo(moveTarget);
    }
    
    private void OnResourceCollected(CollectableResource resource)
    {
        _stateMachine.ChangeState<UnitIdleState>();
        ResourceCollected?.Invoke(this, resource);
    }
    
    private void OnResourceStored(CollectableResource resource)
    {
        _stateMachine.ChangeState<UnitIdleState>();
        ResourceStored?.Invoke(this, resource);
    }
    
    private void OnResourceBaseBuilt()
    {
        _stateMachine.ChangeState<UnitIdleState>();
        ResourceBaseCreated?.Invoke(this);
    }
}
