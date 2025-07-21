using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private int _scanMaxCapacity = 20;
    [SerializeField] private LayerMask _scanLayers;
    [SerializeField] private float _collectDelay = 0.5f;
    [SerializeField] private ParticleSystem _scanEffect;
    
    private WaitForSeconds _collectWaitDelay;
    private float _collectRadius;
    private Collider[] _scanCache;
    
    public event Action<IReadOnlyList<CollectableResource>> ResourcesCollected;
    
    private void Awake()
    {
        float halfDivisor = 2.0f;
        Vector3 halfScale = gameObject.transform.localScale / halfDivisor;
        
        _collectRadius = Mathf.Max(halfScale.x, halfScale.y, halfScale.z);
        _collectWaitDelay = new WaitForSeconds(_collectDelay);
        _scanCache = new Collider[_scanMaxCapacity];
    }
    
    public void Scan()
    {
        if (_scanEffect.isPlaying)
            _scanEffect.Stop();
        
        _scanEffect.Play();
        
        StartCoroutine(Collect());
    }
    
    private IEnumerator Collect()
    {
        yield return _collectWaitDelay;
        CollectResources();
    }
    
    private void CollectResources()
    {
        int foundCount = Physics.OverlapSphereNonAlloc(gameObject.transform.position, _collectRadius, _scanCache, _scanLayers);
        
        if (foundCount == 0)
            return;
        
        var foundResources = new List<CollectableResource>();
        
        for (int i = 0; i < foundCount; i++)
            if (_scanCache[i].TryGetComponent(out CollectableResource resource))
                foundResources.Add(resource);
        
        if (foundResources.Count > 0)
            ResourcesCollected?.Invoke(foundResources);
    }
}
