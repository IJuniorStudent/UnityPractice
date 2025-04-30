using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private Vector3 _growSpeed = new Vector3(0.05f, 0.05f, 0.05f);
    
    void Update()
    {
        transform.localScale += _growSpeed * Time.deltaTime;
    }
}
