using System.Collections.Generic;
using UnityEngine;

public class MultiBoxArea : MonoBehaviour
{
    [SerializeField] private List<BoxArea> _areas;
    
    public Vector3 GetRandomPoint()
    {
        BoxArea area = _areas[UnityEngine.Random.Range(0, _areas.Count)];
        
        return area.GetRandomPoint();
    }
}
