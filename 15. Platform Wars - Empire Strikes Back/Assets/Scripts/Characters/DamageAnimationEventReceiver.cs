using System;
using UnityEngine;

public class DamageAnimationEventReceiver : MonoBehaviour
{
    public event Action DamageAreaActivated;
    
    public void ActivateDamageArea()
    {
        DamageAreaActivated?.Invoke();
    }
}
