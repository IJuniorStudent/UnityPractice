using UnityEngine;

public class SingleShooter : MonoBehaviour
{
    [SerializeField] private string _targetLayerName;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    [SerializeField] private AnimationActivator _blastEffect;
    [SerializeField] private Sprite _projectileSprite;
    
    private int _targetLayer;
    
    private void Awake()
    {
        _targetLayer = LayerMask.NameToLayer(_targetLayerName);
    }
    
    public void Initialize(ProjectileSpawner projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
    }
    
    public void Fire()
    {
        _blastEffect.Play();
        
        Projectile projectile = _projectileSpawner.Spawn(gameObject.transform.position, gameObject.transform.rotation);
        
        projectile.SetSprite(_projectileSprite);
        projectile.gameObject.layer = _targetLayer;
    }
}
