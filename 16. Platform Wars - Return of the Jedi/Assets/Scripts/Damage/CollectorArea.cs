using UnityEngine;

public abstract class CollectorArea : MonoBehaviour
{
    [SerializeField] private float _radius = 5.0f;
    [SerializeField] private LayerMask _searchMask;
    [SerializeField] private int _maxCollectedObjects = 5;
    
    protected Collider2D[] Overlapped;
    private ContactFilter2D _contactFilter;
    
    private void Awake()
    {
        Overlapped = new Collider2D[_maxCollectedObjects];
        
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_searchMask);
        _contactFilter.useLayerMask = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Vector3 position = gameObject.transform.position;
        
        Gizmos.color = GetGizmosColor();
        Gizmos.DrawWireSphere(new Vector3(position.x, position.y, position.z), _radius);
    }
    
    protected int Collect()
    {
        Vector3 position = gameObject.transform.position;
        Vector2 areaCenter = new Vector2(position.x, position.y);
        
        return Physics2D.OverlapCircle(areaCenter, _radius, _contactFilter, Overlapped);
    }
    
    protected abstract Color GetGizmosColor();
}
