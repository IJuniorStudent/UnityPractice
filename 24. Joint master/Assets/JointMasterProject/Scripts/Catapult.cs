using UnityEngine;
using DG.Tweening;

public class Catapult : MonoBehaviour
{
    [SerializeField] private Payload _prefab;
    [SerializeField] private Transform _payloadSpawnPoint;
    [SerializeField] private Rigidbody _armRigidBody;
    [SerializeField] private PayloadLauncher _launcher;
    
    private bool _isReadyToPrepare;
    private bool _isReadyToLaunch;
    
    private void Awake()
    {
        _isReadyToPrepare = true;
        _isReadyToLaunch = false;
    }
    
    private void OnEnable()
    {
        _launcher.Launched += OnPayloadLaunched;
    }
    
    private void OnDisable()
    {
        _launcher.Launched -= OnPayloadLaunched;
    }
    
    public void PrepareShot()
    {
        if (_isReadyToPrepare == false)
            return;
        
        Vector3 launcherAngle = new Vector3(80, 0, 0);
        float prepareDuration = 0.7f;
        
        _isReadyToPrepare = false;
        _armRigidBody.isKinematic = true;
        _armRigidBody.transform.DORotate(launcherAngle, prepareDuration).OnComplete(SpawnPayload);
    }
    
    public void Fire()
    {
        if (_isReadyToLaunch == false)
            return;
        
        _isReadyToLaunch = false;
        _armRigidBody.isKinematic = false;
    }
    
    private void SpawnPayload()
    {
        _isReadyToLaunch = true;
        _launcher.ResetState();
        
        Instantiate(_prefab, _payloadSpawnPoint.position, Quaternion.identity);
    }
    
    private void OnPayloadLaunched()
    {
        _isReadyToPrepare = true;
    }
}
