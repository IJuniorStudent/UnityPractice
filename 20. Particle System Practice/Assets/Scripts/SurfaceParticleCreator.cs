using UnityEngine;

public class SurfaceParticleCreator : ParticleCreator
{
    protected override void OnSurfaceClicked(Vector3 position, Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        Spawner.Spawn(position + direction * SurfaceOffset, rotation);
    }
}
