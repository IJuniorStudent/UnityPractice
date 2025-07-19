using UnityEngine;

public class FlagCreator : MonoBehaviour
{
    private ResourceBaseFoundation _flag;
    private Unit _worker;
    
    public bool IsWorkActive => _worker != null;
    public Vector3 Position => _flag.gameObject.transform.position;
    
    public void Initialize(ResourceBaseFoundation prefab)
    {
        _flag = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        Deactivate();
    }
    
    public void Activate(Vector3 position)
    {
        _flag.gameObject.transform.position = position;
        _flag.gameObject.SetActive(true);
    }
    
    public void SetWorker(Unit worker)
    {
        _worker = worker;
        _worker.MoveTo(_flag.transform);
    }
    
    public void UpdatePosition(Vector3 position)
    {
        Transform targetTransform = _flag.transform;
        targetTransform.position = position;
        _worker.UpdateMoveTarget(targetTransform);
    }
    
    public void Deactivate()
    {
        _worker = null;
        _flag.gameObject.SetActive(false);
    }
}
