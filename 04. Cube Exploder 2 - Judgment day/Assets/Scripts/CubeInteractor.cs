using System.Collections.Generic;
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
        _spawner.Disappeared += OnDisappeared;
    }
    
    private void OnDisable()
    {
        _raycaster.Touched -= OnTouched;
        _spawner.Spawned -= OnSpawned;
        _spawner.Disappeared -= OnDisappeared;
    }
    
    private void OnTouched(Cube cube)
    {
        cube.Touch();
    }
    
    private void OnSpawned(Cube cube, Vector3 parentPosition)
    {
        _exploder.MakeShockWave(parentPosition, cube.gameObject.GetComponent<Rigidbody>());
    }
    
    private void OnDisappeared(Vector3 lastPosition, float explodeRadius, float explodeForceMultiplier)
    {
        List<Cube> cubes = _raycaster.LocateCubes(lastPosition, explodeRadius);
        
        foreach (var cube in cubes)
        {
            Rigidbody target = cube.gameObject.GetComponent<Rigidbody>();
            
            _exploder.MakeShockWaveIndirect(lastPosition, explodeRadius, explodeForceMultiplier, target);
        }
    }
}
