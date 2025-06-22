using System;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    private const string FireAxisName = "Fire1";
    private const string JumpAxisName = "Jump";
    
    private Dictionary<string, bool> _axisHoldState;
    
    public event Action FirePressed;
    public event Action JumpPressed;
    
    private void Awake()
    {
        _axisHoldState = new Dictionary<string, bool>
        {
            { FireAxisName, false },
            { JumpAxisName, false }
        };
    }
    
    private void Update()
    {
        UpdateAxisHoldState(FireAxisName, FirePressed);
        UpdateAxisHoldState(JumpAxisName, JumpPressed);
    }
    
    private void UpdateAxisHoldState(string axisName, Action axisDownEvent)
    {
        float axisValue = Input.GetAxisRaw(axisName);
        
        if (axisValue != 0 && _axisHoldState[axisName] == false)
        {
            _axisHoldState[axisName] = true;
            axisDownEvent?.Invoke();
        }
        
        if (axisValue == 0 && _axisHoldState[axisName])
            _axisHoldState[axisName] = false;
    }
}
