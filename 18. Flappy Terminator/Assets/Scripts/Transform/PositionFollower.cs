using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private void Update()
    {
        gameObject.transform.position = _target.position;
    }
}
