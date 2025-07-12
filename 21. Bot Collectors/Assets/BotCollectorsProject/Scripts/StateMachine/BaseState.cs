public class BaseState
{
    protected Unit Owner;
    
    protected BaseState(Unit owner)
    {
        Owner = owner;
    }
    
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
