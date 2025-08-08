using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MouseMoveTargetSelector _targetSelector;
    [SerializeField] private BaseMover _mover;
    
    private void OnEnable()
    {
        _targetSelector.TargetSelected += OnTargetSelected;
        _mover.TargetReached += OnTargetReached;
    }
    
    private void OnDisable()
    {
        _targetSelector.TargetSelected -= OnTargetSelected;
        _mover.TargetReached -= OnTargetReached;
    }
    
    private void OnTargetSelected(Transform target)
    {
        _mover.MoveTo(target);
    }
    
    private void OnTargetReached()
    {
        _targetSelector.Hide();
    }
}
