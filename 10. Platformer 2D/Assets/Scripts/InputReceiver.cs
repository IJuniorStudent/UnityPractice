using System;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpAxis = "Jump";
    
    private Dictionary<string, bool> _holdState;
    
    public event Action<float> MoveButtonDown;
    public event Action MoveButtonUp;
    public event Action<float> JumpButtonDown;
    
    private void Awake()
    {
        _holdState = new Dictionary<string, bool>
        {
            {HorizontalAxis, false},
            {JumpAxis, false},
        };
    }
    
    private void Update()
    {
        UpdateAxisHoldState(HorizontalAxis, MoveButtonDown, MoveButtonUp);
        UpdateAxisHoldState(JumpAxis, JumpButtonDown, null);
    }
    
    private void UpdateAxisHoldState(string axisName, Action<float> axisDownEvent, Action axisUpEvent)
    {
        float axisDelta = Input.GetAxisRaw(axisName);
        
        if (axisDelta != 0.0f && _holdState[axisName] == false)
        {
            _holdState[axisName] = true;
            axisDownEvent?.Invoke(axisDelta);
        }
        
        if (axisDelta == 0.0f && _holdState[axisName])
        {
            _holdState[axisName] = false;
            axisUpEvent?.Invoke();
        }
    }
}
