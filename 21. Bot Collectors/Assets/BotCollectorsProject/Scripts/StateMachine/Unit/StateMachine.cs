using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, BaseState> _states;
    private BaseState _currentState;
    
    public StateMachine(Unit owner)
    {
        _states = new Dictionary<Type, BaseState>
        {
            [typeof(IdleState)] = new IdleState(owner),
            [typeof(MoveState)] = new MoveState(owner)
        };
    }
    
    public void Update()
    {
        _currentState?.Update();
    }
    
    public bool IsInState<T>() where T : BaseState
    {
        return typeof(T) == _currentState?.GetType();
    }
    
    public void ChangeState<T>() where T : BaseState
    {
        if (_states.TryGetValue(typeof(T), out var state) == false || IsInState<T>())
            return;
        
        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }
}
