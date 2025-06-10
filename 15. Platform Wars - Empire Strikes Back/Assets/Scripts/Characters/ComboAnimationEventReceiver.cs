using System;
using UnityEngine;

public class ComboAnimationEventReceiver : MonoBehaviour
{
    public event Action ComboWindowOpened;
    public event Action ComboWindowClosed;
    public event Action ComboCanBePerformed;
    
    public void OpenComboWindow()
    {
        ComboWindowOpened?.Invoke();
    }
    
    public void TryContinueCombo()
    {
        ComboCanBePerformed?.Invoke();
    }
    
    public void CloseComboWindow()
    {
        ComboWindowClosed?.Invoke();
    }
}
