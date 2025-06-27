using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class LifetimedParticles : MonoBehaviour
{
    private ParticleSystem _particles;
    
    public event Action<LifetimedParticles> Stopped;
    
    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
    }
    
    private void OnParticleSystemStopped()
    {
        Stopped?.Invoke(this);
    }
    
    public void Initialize(Transform[] collisionPlanes)
    {
        foreach (var plane in collisionPlanes)
            _particles.collision.AddPlane(plane);
    }
    
    public void Restart()
    {
        _particles.Simulate(0.0f, true, true);
        _particles.Play();
    }
}
