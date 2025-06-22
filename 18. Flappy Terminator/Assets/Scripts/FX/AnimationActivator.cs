using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationActivator : MonoBehaviour
{
    [SerializeField] private string _animationTriggerName;
    
    private Animator _animator;
    private int _animationTriggerHash;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationTriggerHash = Animator.StringToHash(_animationTriggerName);
    }
    
    public void Play()
    {
        _animator.ResetTrigger(_animationTriggerHash);
        _animator.SetTrigger(_animationTriggerHash);
    }
}
