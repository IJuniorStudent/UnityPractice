using UnityEngine;

public abstract class BaseMutator : MonoBehaviour
{
    [SerializeField] protected float Speed;
    
    private void Start()
    {
        BeginMutate();
    }
    
    protected abstract void BeginMutate();
}
