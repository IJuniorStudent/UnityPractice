using UnityEngine;

public class VolumeExpander : MonoBehaviour
{
    [SerializeField] private Vector3 _growSpeed = new Vector3(0.05f, 0.05f, 0.05f);
    
    private void Update()
    {
        transform.localScale += _growSpeed * Time.deltaTime;
    }
}
