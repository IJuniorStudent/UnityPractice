using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class MoveAnimationController : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private bool _isSpriteMirrored;
    
    private SpriteRenderer _renderer;
    private Animator _animator;
    
    private int _isMovingKeyHash;
    private bool _backwardFlipValue;
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        _isMovingKeyHash = Animator.StringToHash("IsMoving");
        _backwardFlipValue = _isSpriteMirrored == false;
    }
    
    private void OnEnable()
    {
        _mover.MoveForwardStarted += OnMoveForwardStarted;
        _mover.MoveBackwardStarted += OnMoveBackwardStarted;
        _mover.MoveStopped += OnMoveStopped;
    }
    
    private void OnDisable()
    {
        _mover.MoveForwardStarted -= OnMoveForwardStarted;
        _mover.MoveBackwardStarted -= OnMoveBackwardStarted;
        _mover.MoveStopped -= OnMoveStopped;
    }
    
    private void OnMoveForwardStarted()
    {
        _renderer.flipX = !_backwardFlipValue;
        _animator.SetBool(_isMovingKeyHash, true);
    }
    
    private void OnMoveBackwardStarted()
    {
        _renderer.flipX = _backwardFlipValue;
        _animator.SetBool(_isMovingKeyHash, true);
    }
    
    private void OnMoveStopped()
    {
        _animator.SetBool(_isMovingKeyHash, false);
    }
}
