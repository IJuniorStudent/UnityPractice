using UnityEngine;

public class DirectedParticleCreator : ParticleCreator
{
    [SerializeField] private Vector3 _particleSystemRotation;
    
    private Quaternion _rotation;
    
    private void Awake()
    {
        _rotation = Quaternion.Euler(_particleSystemRotation);
    }
    
    protected override void OnSurfaceClicked(Vector3 position, Vector3 direction)
    {
        Spawner.Spawn(position + direction * SurfaceOffset, _rotation);
    }
}
