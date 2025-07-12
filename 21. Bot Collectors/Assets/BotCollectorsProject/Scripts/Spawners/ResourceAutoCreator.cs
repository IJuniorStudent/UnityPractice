using System.Collections;
using UnityEngine;

public class ResourceAutoCreator : MonoBehaviour
{
    [SerializeField] private MultiBoxArea _spawnArea;
    [SerializeField] private ResourceSpawner _spawner;
    [SerializeField] private float _spawnInterval = 5.0f;
    [SerializeField] private int _resourcesCountPerSpawn = 3;
    
    private WaitForSeconds _spawnDelay;
    
    private void Awake()
    {
        _spawnDelay = new WaitForSeconds(_spawnInterval);
    }
    
    private void Start()
    {
        StartCoroutine(SpawnResources());
    }
    
    private IEnumerator SpawnResources()
    {
        while (enabled)
        {
            yield return _spawnDelay;
        
            for (int i = 0; i < _resourcesCountPerSpawn; i++)
            {
                SpawnResource();
                yield return null;
            }
        }
    }
    
    private void SpawnResource()
    {
        Vector3 position = _spawnArea.GetRandomPoint();
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.value * 360.0f, 0);
        
        _spawner.Spawn(position, rotation);
    }
}
