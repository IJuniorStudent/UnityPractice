using System;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private LayerMask _affectLayers;
    [SerializeField] private int _maxCollectedObjects = 5;
    
    private Collider2D[] _overlapped;
    private ContactFilter2D _contactFilter;
    
    public event Action<IDamageable> DamageDealt;
    
    private void Awake()
    {
        _overlapped = new Collider2D[_maxCollectedObjects];
        
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_affectLayers);
        _contactFilter.useLayerMask = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Vector3 position = gameObject.transform.position;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(position.x, position.y, position.z), _radius);
    }
    
    public void Trigger()
    {
        Vector3 position = gameObject.transform.position;
        Vector2 areaCenter = new Vector2(position.x, position.y);
        
        int collectedCount = Physics2D.OverlapCircle(areaCenter, _radius, _contactFilter, _overlapped);
        
        for (int i = 0; i < collectedCount; i++)
        {
            if (_overlapped[i].gameObject.TryGetComponent(out IDamageable damageable))
                DamageDealt?.Invoke(damageable);
        }
    }
}
