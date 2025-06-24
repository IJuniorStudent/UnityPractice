using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(SequentialShooter))]
public class InputShooter : MonoBehaviour
{
    [SerializeField] private InputReceiver _inputReceiver;
    
    private Player _player;
    private SequentialShooter _shooter;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _shooter = GetComponent<SequentialShooter>();
    }
    
    private void OnEnable()
    {
        _inputReceiver.FirePressed += OnFirePressed;
    }
    
    private void OnDisable()
    {
        _inputReceiver.FirePressed -= OnFirePressed;
    }
    
    private void OnFirePressed()
    {
        if (_player.IsAlive)
            _shooter.Fire();
    }
}
