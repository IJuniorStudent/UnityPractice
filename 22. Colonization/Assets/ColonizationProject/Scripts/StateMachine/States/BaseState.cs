public class BaseState<TOwner>
{
    protected TOwner Owner;
    
    protected BaseState(TOwner owner)
    {
        Owner = owner;
    }
    
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}