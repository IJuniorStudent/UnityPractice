using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public event Action<Coin> Collected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
            Collected?.Invoke(coin);
    }
}
