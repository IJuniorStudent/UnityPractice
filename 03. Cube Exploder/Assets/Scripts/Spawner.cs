using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    
    private Vector3[] LocalSpawnPositions = {
        new (-0.25f, -0.25f, -0.25f),
        new (-0.25f, -0.25f,  0.25f),
        new (-0.25f,  0.25f, -0.25f),
        new (-0.25f,  0.25f,  0.25f),
        new ( 0.25f, -0.25f, -0.25f),
        new ( 0.25f, -0.25f,  0.25f),
        new ( 0.25f,  0.25f, -0.25f),
        new ( 0.25f,  0.25f,  0.25f),
    }; 
    
    public event Action<Cube, Vector3> Spawned;
    
    private void Start()
    {
        Spawn(
            new Vector3(0.0f, 1.0f, 0.0f), 
            Quaternion.Euler(0, 45.0f, 0), 
            1.0f, 
            new Vector3(2.0f, 2.0f, 2.0f)
        );
    }
    
    private void OnTouched(Cube cube)
    {
        Transform sourceTransform = cube.gameObject.transform;
        Vector3 sourcePosition = sourceTransform.position;
        Quaternion sourceRotation = sourceTransform.rotation;
        Vector3 sourceScale = sourceTransform.localScale;
        float sourceProbability = cube.ExplodeProbability;

        cube.Clicked -= OnTouched;
        Destroy(cube.gameObject);

        if (UnityEngine.Random.value > sourceProbability)
            return;
        
        float divisor = 2.0f;
        Vector3[] spawnPositions = GeneratePositions(cube.gameObject.transform);
        
        foreach (var position in spawnPositions)
        {
            Cube child = Spawn(position, sourceRotation, sourceProbability / divisor, sourceScale / divisor);
            
            Spawned?.Invoke(child, sourcePosition);
        }
    }
    
    private Cube Spawn(Vector3 position, Quaternion rotation, float explodeProbability, Vector3 scale)
    {
        Cube cube = Instantiate(_prefab, position, rotation);
        
        cube.SetProperties(explodeProbability, scale, GetRandomColor());
        cube.Clicked += OnTouched;
        
        return cube;
    }

    private Vector3[] GeneratePositions(Transform sourceTransform)
    {
        int minCount = 2;
        int maxCount = 6;
        var indices = new int[LocalSpawnPositions.Length];
        
        for (int i = 0; i < LocalSpawnPositions.Length; i++)
            indices[i] = i;

        Shuffle(indices);
        
        int positionsCount = UnityEngine.Random.Range(minCount, maxCount);
        var positions = new Vector3[positionsCount];
        
        for (int i = 0; i < positionsCount; i++)
            positions[i] = sourceTransform.TransformPoint(LocalSpawnPositions[indices[i]]);
        
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
}
