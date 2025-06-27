using UnityEngine;

public class ParticleCreator : MonoBehaviour
{
    private const float SurfaceOffset = 0.3f;
    
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private ParticleSpawner _spawner;
    
    private void OnEnable()
    {
        _raycaster.SurfaceClicked += OnSurfaceClicked;
    }
    
    private void OnDisable()
    {
        _raycaster.SurfaceClicked -= OnSurfaceClicked;
    }
    
    private void OnSurfaceClicked(Vector3 position, Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        _spawner.Spawn(position + direction * SurfaceOffset, rotation);
    }
}
