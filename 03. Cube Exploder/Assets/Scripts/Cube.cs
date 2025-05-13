using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> Clicked;
    
    public float ExplodeProbability { get; private set; } = 1.0f;

    public void SetProperties(float explodeProbability, Vector3 scale, Color color)
    {
        ExplodeProbability = explodeProbability;
        transform.localScale = scale;
        GetComponent<MeshRenderer>().material.color = color;
    }
    
    public void Touch()
    {
        Clicked?.Invoke(this);
    }
}
