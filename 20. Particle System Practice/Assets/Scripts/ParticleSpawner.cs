using UnityEngine;
using UnityEngine.Pool;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private LifetimedParticles _prefab;
    [SerializeField] private Transform[] _particlesCollisionPlanes;
    
    private ObjectPool<LifetimedParticles> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<LifetimedParticles>(
            createFunc: CreateObject,
            actionOnGet: ReuseObject,
            actionOnRelease: DisableObject,
            actionOnDestroy: DestroyObject,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }
    
    public LifetimedParticles Spawn(Vector3 position, Quaternion rotation)
    {
        LifetimedParticles particles = _pool.Get();
        
        particles.transform.position = position;
        particles.transform.rotation = rotation;
        particles.Restart();
        
        return particles;
    }
    
    protected void Despawn(LifetimedParticles particles)
    {
        _pool.Release(particles);
    }
    
    private LifetimedParticles CreateObject()
    {
        LifetimedParticles particles = Instantiate(_prefab);
        
        particles.Initialize(_particlesCollisionPlanes);
        particles.Stopped += OnStopped;
        
        return particles;
    }
    
    private void ReuseObject(LifetimedParticles particles)
    {
        particles.gameObject.SetActive(true);
    }
    
    private void DisableObject(LifetimedParticles particles)
    {
        particles.gameObject.SetActive(false);
    }
    
    private void DestroyObject(LifetimedParticles particles)
    {
        particles.Stopped -= OnStopped;
        Destroy(particles.gameObject);
    }
    
    private void OnStopped(LifetimedParticles particles)
    {
        _pool.Release(particles);
    }
}
