using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class SpawnDirector : MonoBehaviour
{
    private List<Spawner> _spawners = new ();
    private Timer _timer;
    
    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }
    
    private void OnEnable()
    {
        _timer.Ticked += OnTicked;
    }
    
    private void OnDisable()
    {
        _timer.Ticked -= OnTicked;
    }
    
    public void RegisterSpawner(Spawner spawner)
    {
        _spawners.Add(spawner);
    }
    
    private void OnTicked()
    {
        if (_spawners.Count == 0)
            return;
        
        Spawner spawner = _spawners[Random.Range(0, _spawners.Count)];
        
        spawner.SpawnEnemy();
    }
}
