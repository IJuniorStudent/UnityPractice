using UnityEngine;

[RequireComponent(typeof(ProjectileCollisionTarget))]
public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private ExplodeAnimationEventReceiver _explodeEventReceiver;
    [SerializeField] private AnimationActivator _explodeAnimation;
    [SerializeField] private SpriteRenderer _characterRenderer;
    [SerializeField] private Collider2D _characterCollider;
    [SerializeField] private Rigidbody2D _characterRigidbody;
    [SerializeField] private RigidbodyType2D _defaultBodyType;
    
    private ProjectileCollisionTarget _collisionTarget;
    
    public bool IsAlive { get; protected set; }
    
    private void Awake()
    {
        _collisionTarget = GetComponent<ProjectileCollisionTarget>();
        IsAlive = true;
        
        Awoke();
    }
    
    private void OnEnable()
    {
        _collisionTarget.Collided += OnProjectileCollided;
        _explodeEventReceiver.ExplodeStarted += OnExplodeStarted;
        _explodeEventReceiver.Exploded += OnExploded;
        _explodeEventReceiver.ExplodeFinished += OnExplodeFinished;
        
        Enabled();
    }
    
    private void OnDisable()
    {
        _collisionTarget.Collided -= OnProjectileCollided;
        _explodeEventReceiver.ExplodeStarted -= OnExplodeStarted;
        _explodeEventReceiver.Exploded -= OnExploded;
        _explodeEventReceiver.ExplodeFinished -= OnExplodeFinished;
        
        Disabled();
    }
    
    public void Explode()
    {
        IsAlive = false;
        _explodeAnimation.Play();
        DisableInteraction();
    }
    
    protected abstract void Awoke();
    protected abstract void Enabled();
    protected abstract void Disabled();
    protected abstract void OnExplodeStarted();
    protected abstract void OnExplodeFinished();
    
    protected void EnableInteraction()
    {
        _characterCollider.enabled = true;
        _characterRenderer.enabled = true;
        _characterRigidbody.bodyType = _defaultBodyType;
    }
    
    private void DisableInteraction()
    {
        _characterCollider.enabled = false;
        _characterRigidbody.bodyType = RigidbodyType2D.Static;
    }
    
    private void OnProjectileCollided()
    {
        if (IsAlive)
            Explode();
    }
    
    private void OnExploded()
    {
        _characterRenderer.enabled = false;
    }
}
