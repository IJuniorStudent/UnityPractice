using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _explodePower = 1000.0f;
    
    private float _explodeProbability = 1.0f;
    
    public void Explode()
    {
        if (Random.value < _explodeProbability)
            SpawnMinions();
        
        Destroy(gameObject);
    }

    private void SpawnMinions()
    {
        Vector3[] spawnPositions = GeneratePositions();
        
        for (int i = 0; i < spawnPositions.Length; i++)
            SpawnChild(spawnPositions[i]);
    }

    private void SpawnChild(Vector3 position)
    {
        float divisor = 2.0f;
        
        var child = Instantiate(_cubePrefab, position, transform.rotation);
        Vector3 forceVector = position + (position - transform.position).normalized * _explodePower;

        child.transform.localScale = transform.localScale / divisor;
        child.GetComponent<Exploder>()._explodeProbability = _explodeProbability / divisor;
        child.GetComponent<MeshRenderer>().material.color = GetRandomColor();
        child.GetComponent<Rigidbody>().AddForce(forceVector);
    }
    
    private Color GetRandomColor()
    {
        return Color.HSVToRGB(Random.Range(0.0f, 1.0f), 0.32f, 0.86f);
    }
    
    private Vector3[] GeneratePositions()
    {
        int minCount = 2;
        int maxCount = 6;
        
        int childrenCount = transform.childCount;
        var childIndices = new int[childrenCount];
        
        for (int i = 0; i < childrenCount; i++)
            childIndices[i] = i;
        
        ShuffleArray(childIndices);
        
        int positionsCount = Random.Range(minCount, maxCount + 1);
        var positions = new Vector3[positionsCount];
        
        for (int i = 0; i < positionsCount; i++)
        {
            var childTransform = transform.GetChild(childIndices[i]).transform;
            positions[i] = transform.TransformPoint(childTransform.localPosition);
        }
        
        return positions;
    }
    
    private void ShuffleArray<T>(T[] values)
    {
        int arrayLength = values.Length;
        
        for (int i = 0; i < values.Length; i++)
        {
            int swapIndex = Random.Range(i, arrayLength);
            
            (values[i], values[swapIndex]) = (values[swapIndex], values[i]);
        }
    }
}
