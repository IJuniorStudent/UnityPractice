using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> Interacted;
    public event Action<Cube> InteractedWithSplit;
    
    public float ExplodeProbability { get; private set; } = 1.0f;

    public void Initialize(float explodeProbability, Vector3 scale, Color color)
    {
        ExplodeProbability = explodeProbability;
        transform.localScale = scale;
        GetComponent<MeshRenderer>().material.color = color;
    }
    
    public void Touch()
    {
        if (UnityEngine.Random.value > ExplodeProbability)
            Interacted?.Invoke(this);
        else
            InteractedWithSplit?.Invoke(this);
    }
}
