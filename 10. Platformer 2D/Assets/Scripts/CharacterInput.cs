using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class CharacterInput : MonoBehaviour
{
    [SerializeField] private Mover _characterMover;
    [SerializeField] private Jumper _characterJumper;
    
    private InputReceiver _receiver;
    
    private void Awake()
    {
        _receiver = GetComponent<InputReceiver>();
    }
    
    private void OnEnable()
    {
        _receiver.MoveButtonDown += OnMovePressed;
        _receiver.MoveButtonUp += OnMoveReleased;
        _receiver.JumpButtonDown += OnJumpPressed;
    }
    
    private void OnDisable()
    {
        _receiver.MoveButtonDown -= OnMovePressed;
        _receiver.MoveButtonUp -= OnMoveReleased;
        _receiver.JumpButtonDown -= OnJumpPressed;
    }
    
    private void OnMovePressed(float axisDelta)
    {
        _characterMover.StartMove(axisDelta);
    }
    
    private void OnMoveReleased()
    {
        _characterMover.StopMove();
    }
    
    private void OnJumpPressed(float axisDelta)
    {
        _characterJumper.Jump();
    }
}
