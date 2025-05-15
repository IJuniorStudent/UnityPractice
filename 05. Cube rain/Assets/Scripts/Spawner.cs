using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FirstCollisionDetector _prefab;
    [SerializeField] private int _cubesCapacity = 10;
    [SerializeField] private int _cubesMaxCount = 20;
    
    private BoxArea _boxArea;
    private ObjectPool<FirstCollisionDetector> _pool;
    private float _releaseDelaySecondsMin = 2.0f;
    private float _releaseDelaySecondsMax = 5.0f;
    
    private void Awake()
    {
        _boxArea = GetComponent<BoxArea>();
        
        _pool = new ObjectPool<FirstCollisionDetector>(
            createFunc: CreateCube,
            actionOnGet: ReuseCube,
            actionOnRelease: DisableCube,
            actionOnDestroy: DestroyCube,
            collectionCheck: true,
            defaultCapacity: _cubesCapacity,
            maxSize: _cubesMaxCount
        );
    }

    private void Start()
    {
        for (int i = 0; i < _cubesCapacity; i++)
            _pool.Get();
    }
    
    private FirstCollisionDetector CreateCube()
    {
        FirstCollisionDetector cube = Instantiate(_prefab);
        RandomizeStartPosition(cube.gameObject.transform);
        
        cube.Collided += OnCollided;
        
        return cube;
    }
    
    private void DestroyCube(FirstCollisionDetector detector)
    {
        detector.Collided -= OnCollided;
        
        Destroy(detector.gameObject);
    }

    private void ReuseCube(FirstCollisionDetector detector)
    {
        RandomizeStartPosition(detector.gameObject.transform);
        
        detector.gameObject.GetComponent<ColorRandomizer>().Randomize();
        detector.gameObject.SetActive(true);
    }

    private void DisableCube(FirstCollisionDetector detector)
    {
        detector.gameObject.SetActive(false);
    }

    private void RandomizeStartPosition(Transform targetTransform)
    {
        targetTransform.position = _boxArea.GetRandomPoint();
        targetTransform.rotation = Quaternion.Euler(0, Random.Range(0, 360), Random.Range(0, 360));
    }
    
    private void OnCollided(FirstCollisionDetector detector)
    {
        detector.gameObject.GetComponent<ColorRandomizer>().Randomize();
        
        float releaseDelay = Random.Range(_releaseDelaySecondsMin, _releaseDelaySecondsMax);
        
        StartCoroutine(RespawnDelayed(detector, releaseDelay));
    }

    private IEnumerator RespawnDelayed(FirstCollisionDetector target, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        
        _pool.Release(target);
        _pool.Get();
    }
}
