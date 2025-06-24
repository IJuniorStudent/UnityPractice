using System;
using UnityEngine;

public class SingleShooter : MonoBehaviour
{
    [SerializeField] private AnimationActivator _blastEffect;
    
    public event Action<Vector3, Quaternion> Shoot;
    
    public void Fire()
    {
        _blastEffect.Play();
        
        Shoot?.Invoke(gameObject.transform.position, gameObject.transform.rotation);
    }
}
