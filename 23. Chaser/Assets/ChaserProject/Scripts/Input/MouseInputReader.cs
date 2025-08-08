using System;
using UnityEngine;

public class MouseInputReader : MonoBehaviour
{
    private const int MouseLeftButtonId = 0;
    
    public event Action MouseLeftButtonClicked;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseLeftButtonId))
            MouseLeftButtonClicked?.Invoke();
    }
}
