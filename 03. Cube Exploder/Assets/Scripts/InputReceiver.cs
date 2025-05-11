using System;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    private const int MouseButtonId = 0;
    
    public event Action MouseButtonPressed;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButtonId))
            MouseButtonPressed?.Invoke();
    }
}
