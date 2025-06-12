using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
[RequireComponent(typeof(ColorRandomizer))]
public class Cube : LifeLimitedEntity
{
    private CollisionDetector _detector;
    private ColorRandomizer _colorRandomizer;
    
    private void Awake()
    {
        _detector = GetComponent<CollisionDetector>();
        _colorRandomizer = GetComponent<ColorRandomizer>();
    }
    
    private void OnEnable()
    {
        _detector.Collided += OnCollided;
    }
    
    private void OnDisable()
    {
        _detector.Collided -= OnCollided;
    }
    
    private void OnCollided()
    {
        _colorRandomizer.Change();
        
        StartCoroutine(CountdownLifetime());
    }
    
    public override void Reset()
    {
        _detector.Reset();
        _colorRandomizer.Change();
    }
    
    private IEnumerator CountdownLifetime()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(LifetimeMin, LifetimeMax));
        InvokeLifetimeExpired(this);
    }
}
