using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Raycaster), typeof(Exploder))]
public class CubeInteractor : MonoBehaviour
{
    private Spawner _spawner;
    private Raycaster _raycaster;
    private Exploder _exploder;
    
    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _raycaster = GetComponent<Raycaster>();
        _exploder = GetComponent<Exploder>();
    }
    
    private void OnEnable()
    {
        _raycaster.Touched += OnTouched;
        _spawner.Spawned += OnSpawned;
    }
    
    private void OnDisable()
    {
        _raycaster.Touched -= OnTouched;
        _spawner.Spawned -= OnSpawned;
    }
    
    private void OnTouched(Cube cube)
    {
        cube.Touch();
    }
    
    private void OnSpawned(Cube cube, Vector3 parentPosition)
    {
        _exploder.MakeShockWave(parentPosition, cube.gameObject.GetComponent<Rigidbody>());
    }
}
