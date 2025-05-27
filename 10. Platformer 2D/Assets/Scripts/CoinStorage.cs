using UnityEngine;

public class CoinStorage : MonoBehaviour
{
    [SerializeField] private CoinCollector _collector;
    
    private void OnEnable()
    {
        _collector.Collected += OnCollected;
    }
    
    private void OnDisable()
    {
        _collector.Collected -= OnCollected;
    }
    
    private void OnCollected(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }
}
