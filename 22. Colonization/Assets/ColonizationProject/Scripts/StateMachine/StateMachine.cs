using System;
using System.Collections.Generic;

public class StateMachine<TOwner>
{
    private Dictionary<Type, BaseState<TOwner>> _states;
    private BaseState<TOwner> _currentState;
    
    public StateMachine(Dictionary<Type, BaseState<TOwner>> states)
    {
        _states = states;
    }
    
    public void Update()
    {
        _currentState?.Update();
    }
    
    public bool IsInState<TState>() where TState : BaseState<TOwner>
    {
        return typeof(TState) == _currentState?.GetType();
    }
    
    public void ChangeState<TState>() where TState : BaseState<TOwner>
    {
        if (_states.TryGetValue(typeof(TState), out var state) == false || IsInState<TState>())
            return;
        
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
    
    public void PerformCurrentState(Action<BaseState<TOwner>> callback)
    {
        if (_currentState != null)
            callback(_currentState);
    }
}
