using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCreator : GenericCreator<CollectableResource>
{
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
    
    private void OnEnable()
    {
        _spawner.ObjectCreated += OnResourceCreated;
        _spawner.ObjectDestroyed += OnResourceDestroyed;
    }
    
    private void OnDisable()
    {
        _spawner.ObjectCreated -= OnResourceCreated;
        _spawner.ObjectDestroyed -= OnResourceDestroyed;
    }
    
    public override bool TryFree(CollectableResource resource)
    {
        bool isFreed = base.TryFree(resource);
        
        if (isFreed)
        {
            resource.ResetState();
            _spawner.Despawn(resource);
        }
        
        return isFreed;
    }
    
    public List<CollectableResource> FilterFreeSortedResources(IReadOnlyList<CollectableResource> resources, Vector3 center)
    {
        var filtered = new List<CollectableResource>();
        
        foreach (CollectableResource resource in resources)
            if (IsFree(resource))
                filtered.Add(resource);
        
        filtered.Sort((resourceA, resourceB) =>
            (center - resourceA.transform.position).sqrMagnitude.CompareTo(
                (center - resourceB.transform.position).sqrMagnitude)
        );
        
        return filtered;
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
        Vector3 position = SpawnArea.GetRandomPoint();
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.value * MaxInitialRotationAngle, 0);
        
        _spawner.Spawn(position, rotation);
    }
    
    private void OnResourceCreated(CollectableResource resource)
    {
        Register(resource);
    }
    
    private void OnResourceDestroyed(CollectableResource resource)
    {
        Unregister(resource);
    }
}
