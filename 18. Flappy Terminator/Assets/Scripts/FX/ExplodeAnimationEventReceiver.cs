using System;
using UnityEngine;

public class ExplodeAnimationEventReceiver : MonoBehaviour
{
    public event Action ExplodeStarted;
    public event Action Exploded;
    public event Action ExplodeFinished;
    
    public void StartExplode()
    {
        ExplodeStarted?.Invoke();
    }
    
    public void Explode()
    {
        Exploded?.Invoke();
    }
    
    public void FinishExplode()
    {
        ExplodeFinished?.Invoke();
    }
}
