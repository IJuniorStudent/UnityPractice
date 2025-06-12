using UnityEngine;
using TMPro;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;
    [SerializeField] private TMP_Text _spawnedCountText;
    [SerializeField] private TMP_Text _createdCountText;
    [SerializeField] private TMP_Text _activeCountText;
    
    private int _spawnedTotal;
    private int _createdCount;
    private int _activeCount;
    
    private void OnEnable()
    {
        _spawner.ObjectSpawned += OnSpawned;
        _spawner.ObjectCreated += OnCreated;
        _spawner.ObjectActivated += OnActivated;
        _spawner.ObjectDeactivated += OnDeactivated;
    }
    
    private void OnDisable()
    {
        _spawner.ObjectSpawned -= OnSpawned;
        _spawner.ObjectCreated -= OnCreated;
        _spawner.ObjectActivated -= OnActivated;
        _spawner.ObjectDeactivated -= OnDeactivated;
    }
    
    private void OnSpawned()
    {
        _spawnedTotal++;
        _spawnedCountText.text = _spawnedTotal.ToString();
    }
    
    private void OnCreated()
    {
        _createdCount++;
        _createdCountText.text = _createdCount.ToString();
    }
    
    private void OnActivated()
    {
        _activeCount++;
        _activeCountText.text = _activeCount.ToString();
    }
    
    private void OnDeactivated()
    {
        _activeCount--;
        _activeCountText.text = _activeCount.ToString();
    }
}
