using System;
using UnityEngine;

[RequireComponent(typeof(ResourceCollector))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    
    private StateMachine _stateMachine;
    private ResourceCollector _collector;
    
    public Transform MoveTarget { get; private set; }
    public float MoveSpeed => _moveSpeed;
    
    public event Action<Unit> ResourceCollected;
    public event Action<Unit> ResourceStored;
    
    private void Awake()
    {
        _collector = GetComponent<ResourceCollector>();
        
        _stateMachine = new StateMachine(this);
        _stateMachine.ChangeState<IdleState>();
    }
    
    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
        _collector.ResourceStored += OnResourceStored;
    }
    
    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
        _collector.ResourceStored -= OnResourceStored;
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }
    
    public void CollectResource(CollectableResource resource)
    {
        if (_stateMachine.IsInState<IdleState>() == false)
            return;
        
        _collector.SetCollectTarget(resource);
        MoveTo(resource.gameObject.transform);
    }
    
    public void MoveTo(Transform resourceTransform)
    {
        if (_stateMachine.IsInState<IdleState>() == false)
            return;
        
        MoveTarget = resourceTransform;
        _stateMachine.ChangeState<MoveState>();
    }
    
    private void OnResourceCollected(ResourceCollector collector)
    {
        _stateMachine.ChangeState<IdleState>();
        ResourceCollected?.Invoke(this);
    }
    
    private void OnResourceStored(ResourceCollector collector)
    {
        _stateMachine.ChangeState<IdleState>();
        ResourceStored?.Invoke(this);
    }
}
