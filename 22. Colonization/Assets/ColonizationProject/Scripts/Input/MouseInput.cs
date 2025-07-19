using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private const int ButtonLeftId = 0;
    
    public event Action LeftClicked;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(ButtonLeftId))
            LeftClicked?.Invoke();
    }
}
