using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TransparencyChanger))]
public class Bomb : LifeLimitedEntity
{
    private const float TransparencyMin = 0.0f;
    private const float TransparencyMax = 1.0f;
    
    private TransparencyChanger _transparencyChanger;
    
    private void Awake()
    {
        _transparencyChanger = GetComponent<TransparencyChanger>();
    }
    
    public void Ignite()
    {
        StartCoroutine(CountdownExplode());
    }
    
    public override void Reset()
    {
        _transparencyChanger.SetValue(TransparencyMax);
    }
    
    private IEnumerator CountdownExplode()
    {
        float lifetime = Random.Range(LifetimeMin, LifetimeMax);
        float fadePerSecond = 1.0f / lifetime;
        
        while (_transparencyChanger.Value > TransparencyMin)
        {
            float newValue = Mathf.MoveTowards(_transparencyChanger.Value, TransparencyMin, fadePerSecond * Time.deltaTime);
            
            _transparencyChanger.SetValue(newValue);
            yield return null;
        }
        
        InvokeLifetimeExpired(this);
    }
}
