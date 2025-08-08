using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [SerializeField] private ParticleSystem _targetEffect;
    
    public void SetPosition(Vector3 position, Vector3 normal)
    {
        Transform selfTransform = gameObject.transform;
        
        selfTransform.position = position;
        selfTransform.up = normal;
    }
    
    public void Show()
    {
        _targetEffect.Clear();
        _targetEffect.Play();
    }
    
    public void Hide()
    {
        _targetEffect.Clear();
        _targetEffect.Stop();
    }
}
