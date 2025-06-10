using System;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpAxis = "Jump";
    private const string FireAxis = "Fire1";
    
    private Dictionary<string, bool> _axisHoldState;
    
    public event Action<float> MovePressed;
    public event Action MoveReleased;
    public event Action<float> JumpPressed;
    public event Action<float> FirePressed;
    
    private void Awake()
    {
        _axisHoldState = new Dictionary<string, bool>
        {
            { HorizontalAxis, false },
            { JumpAxis, false },
            { FireAxis, false }
        };
    }
    
    private void Update()
    {
        UpdateAxisHoldState(HorizontalAxis, MovePressed, MoveReleased);
        UpdateAxisHoldState(JumpAxis, JumpPressed, null);
        UpdateAxisHoldState(FireAxis, FirePressed, null);
    }
    
    private void UpdateAxisHoldState(string axisName, Action<float> axisDownEvent, Action axisUpEvent)
    {
        float axisValue = Input.GetAxisRaw(axisName);
        
        if (axisValue != 0 && _axisHoldState[axisName] == false)
        {
            _axisHoldState[axisName] = true;
            axisDownEvent?.Invoke(axisValue);
        }
        
        if (axisValue == 0 && _axisHoldState[axisName])
        {
            _axisHoldState[axisName] = false;
            axisUpEvent?.Invoke();
        }
    }
}
