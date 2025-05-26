using System;
using UnityEngine;

public class MoveTargetVisualizer : MonoBehaviour
{
    [SerializeField] private TargetEffect _targetPrefab;
    
    private void Awake()
    {
        _targetPrefab.gameObject.SetActive(false);
    }
    
    public void Show(Vector3 target)
    {
        _targetPrefab.transform.position = target;
        _targetPrefab.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        _targetPrefab.gameObject.SetActive(false);
    }
}
