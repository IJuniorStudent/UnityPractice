using System;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class MoveTargetNotifier : MonoBehaviour
{
    [SerializeField] private CharacterMover _character;
    [SerializeField] private MoveTargetVisualizer _visualizer;
    
    private Raycaster _raycaster;
    
    private void Awake()
    {
        _raycaster = GetComponent<Raycaster>();
    }
    
    private void OnEnable()
    {
        _raycaster.MoveTargetSelected += OnMoveTargetSelected;
        _character.TargetReached += OnCharacterReachedTarget;
    }
    
    private void OnDisable()
    {
        _raycaster.MoveTargetSelected -= OnMoveTargetSelected;
        _character.TargetReached -= OnCharacterReachedTarget;
    }
    
    private void OnMoveTargetSelected(Vector3 target)
    {
        _visualizer.Show(target);
        _character.MoveTo(target);
    }
    
    private void OnCharacterReachedTarget()
    {
        _visualizer.Hide();
    }
}
