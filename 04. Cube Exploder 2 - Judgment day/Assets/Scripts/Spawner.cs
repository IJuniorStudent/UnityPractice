using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    
    private Vector3[] _localSpawnPositions; 
    
    public event Action<Cube, Vector3> Spawned;
    public event Action<Vector3, float, float> Disappeared;
    
    private void Start()
    {
        _localSpawnPositions = InitLocalPositions();
        
        SpawnInitialCubes();
    }
    
    private void OnInteracted(Cube cube)
    {
        Vector3 sourcePosition = cube.gameObject.transform.position;
        float radius = cube.ExplodeRadius;
        float forceMultiplier = cube.ExplodeForceMultiplier;

        Despawn(cube);
        
        Disappeared?.Invoke(sourcePosition, radius, forceMultiplier);
    }

    private void OnInteractedWithSplit(Cube cube)
    {
        Transform sourceTransform = cube.gameObject.transform;
        Vector3 sourcePosition = sourceTransform.position;
        Quaternion sourceRotation = sourceTransform.rotation;
        Vector3 sourceScale = sourceTransform.localScale;
        float sourceProbability = cube.ExplodeProbability;
        
        Despawn(cube);
        
        float divisor = 2.0f;
        Vector3[] spawnPositions = GeneratePositions(sourceTransform);
        
        foreach (var position in spawnPositions)
        {
            Cube child = Spawn(position, sourceRotation, sourceProbability / divisor, sourceScale / divisor);
            
            Spawned?.Invoke(child, sourcePosition);
        }
    }
    
    private Cube Spawn(Vector3 position, Quaternion rotation, float explodeProbability, Vector3 scale)
    {
        Cube cube = Instantiate(_prefab, position, rotation);
        
        cube.Initialize(explodeProbability, scale, GetRandomColor());
        cube.Interacted += OnInteracted;
        cube.InteractedWithSplit += OnInteractedWithSplit;
        
        return cube;
    }

    private void Despawn(Cube cube)
    {
        cube.Interacted -= OnInteracted;
        cube.InteractedWithSplit -= OnInteractedWithSplit;
        
        Destroy(cube.gameObject);
    }

    private Vector3[] GeneratePositions(Transform sourceTransform)
    {
        int minCount = 2;
        int maxCount = 6;
        var indices = new int[_localSpawnPositions.Length];
        
        for (int i = 0; i < _localSpawnPositions.Length; i++)
            indices[i] = i;

        Shuffle(indices);
        
        int positionsCount = UnityEngine.Random.Range(minCount, maxCount + 1);
        var positions = new Vector3[positionsCount];
        
        for (int i = 0; i < positionsCount; i++)
            positions[i] = sourceTransform.TransformPoint(_localSpawnPositions[indices[i]]);
        
        return positions;
    }
    
    private void Shuffle<T>(T[] values)
    {
        int arrayLength = values.Length;
        
        for (int i = 0; i < values.Length; i++)
        {
            int swapIndex = UnityEngine.Random.Range(i, arrayLength);
            (values[i], values[swapIndex]) = (values[swapIndex], values[i]);
        }
    }
    
    private Color GetRandomColor()
    {
        return Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), 0.32f, 0.86f);
    }
    
    private void SpawnInitialCubes()
    {
        float initialScale = 2.0f;
        float initialCenterHeight = initialScale / 2.0f;
        float initialExplodeProbability = 1.0f;
        
        var spawnPosition = new Vector3(0.0f, initialCenterHeight, 0.0f);
        var scale = Vector3.one * initialScale;
        
        Spawn(
            spawnPosition,
            Quaternion.Euler(0, 45.0f, 0),
            initialExplodeProbability,
            scale
        );
    }
    
    private Vector3[] InitLocalPositions()
    {
        return new Vector3[]{
            new (-0.25f, -0.25f, -0.25f),
            new (-0.25f, -0.25f,  0.25f),
            new (-0.25f,  0.25f, -0.25f),
            new (-0.25f,  0.25f,  0.25f),
            new ( 0.25f, -0.25f, -0.25f),
            new ( 0.25f, -0.25f,  0.25f),
            new ( 0.25f,  0.25f, -0.25f),
            new ( 0.25f,  0.25f,  0.25f),
        };
    }
}
