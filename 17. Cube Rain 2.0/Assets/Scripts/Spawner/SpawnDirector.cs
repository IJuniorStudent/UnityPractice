using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Detonator))]
public class SpawnDirector : MonoBehaviour
{
    [SerializeField] private BoxArea _boxArea;
    [SerializeField] private int _initialSpawnCount = 20;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    
    private Detonator _detonator;
    
    private void Awake()
    {
        _detonator = GetComponent<Detonator>();
    }
    
    private void Start()
    {
        StartCoroutine(SpawnCubes(_initialSpawnCount));
    }
    
    private void OnEnable()
    {
        _cubeSpawner.ObjectLifetimeExpired += OnCubeExpired;
        _bombSpawner.ObjectLifetimeExpired += OnBombExpired;
    }
    
    private void OnDisable()
    {
        _cubeSpawner.ObjectLifetimeExpired -= OnCubeExpired;
        _bombSpawner.ObjectLifetimeExpired -= OnBombExpired;
    }
    
    private IEnumerator SpawnCubes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnCube();
            yield return null;
        }
    }
    
    private void SpawnCube()
    {
        _cubeSpawner.Spawn(_boxArea.GetRandomPosition(), GetRandomRotation());
    }
    
    private Quaternion GetRandomRotation()
    {
        float angleBounds = 360.0f;
        
        return Quaternion.Euler(Random.value * angleBounds, 0.0f, Random.value * angleBounds);
    }
    
    private void OnCubeExpired(Cube cube)
    {
        Vector3 position = cube.gameObject.transform.position;
        
        _cubeSpawner.Despawn(cube);
        
        Bomb bomb = _bombSpawner.Spawn(position, Quaternion.identity);
        bomb.Ignite();
    }
    
    private void OnBombExpired(Bomb bomb)
    {
        Vector3 position = bomb.gameObject.transform.position;
        
        _bombSpawner.Despawn(bomb);
        _detonator.Explode(position);
        
        SpawnCube();
    }
}
