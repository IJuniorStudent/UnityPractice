using UnityEngine;

public abstract class ParticleCreator : MonoBehaviour
{
    protected const float SurfaceOffset = 0.3f;
    
    [SerializeField] protected ParticleSpawner Spawner;
    [SerializeField] private Raycaster _raycaster;
    
    private void OnEnable()
    {
        _raycaster.SurfaceClicked += OnSurfaceClicked;
    }
    
    private void OnDisable()
    {
        _raycaster.SurfaceClicked -= OnSurfaceClicked;
    }
    
    protected abstract void OnSurfaceClicked(Vector3 position, Vector3 direction);
}
