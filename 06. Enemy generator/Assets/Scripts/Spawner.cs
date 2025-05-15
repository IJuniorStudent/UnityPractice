using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Timer))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private DirectedPoint[] _spawnPositions;
    
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
    
    private void OnTicked()
    {
        DirectedPoint spawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        
        SpawnEnemy(spawnPosition);
    }

    private void SpawnEnemy(DirectedPoint spawnPosition)
    {
        Enemy enemy = Instantiate(_prefab, spawnPosition.Point, spawnPosition.GetRotation());
        
        enemy.StartMove(spawnPosition.Direction);
    }
    
    private void OnDrawGizmos()
    {
        if (_spawnPositions.Length > 0)
            foreach (var point in _spawnPositions)
                DrawPoint(point);
    }

    private void DrawPoint(DirectedPoint point)
    {
        float rayLength = 5.0f;
        Vector3 target = point.Point + point.Direction * rayLength;
        
        Gizmos.color = new Color(0.7f, 0.7f, 0, 1);
        Gizmos.DrawLine(point.Point, target);
        
        Gizmos.color = new Color(0, 0.7f, 0, 1);
        Gizmos.DrawSphere(point.Point, 0.1f);
    }
}
