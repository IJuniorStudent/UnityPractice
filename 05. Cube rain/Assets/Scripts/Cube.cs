using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FirstCollisionDetector), typeof(ColorRandomizer), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.white;
    [SerializeField] private float _disappearTimeMin = 2.0f;
    [SerializeField] private float _disappearTimeMax = 5.0f;
    
    private FirstCollisionDetector _detector;
    private ColorRandomizer _colorRandomizer;
    private MeshRenderer _renderer;
    
    public event Action<Cube> LifetimeExpired;
    
    private void Awake()
    {
        _detector = GetComponent<FirstCollisionDetector>();
        _colorRandomizer = GetComponent<ColorRandomizer>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _detector.Collided += OnCollided;
        _detector.Reset();
        
        _renderer.material.color = _initialColor;
    }

    private void OnDisable()
    {
        _detector.Collided -= OnCollided;
    }
    
    private void OnCollided()
    {
        _renderer.material.color = _colorRandomizer.GetRandomColor();
        
        StartCoroutine(WaitLifetimeExpire());
    }

    private IEnumerator WaitLifetimeExpire()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_disappearTimeMin, _disappearTimeMax));
        
        LifetimeExpired?.Invoke(this);
    }
}
