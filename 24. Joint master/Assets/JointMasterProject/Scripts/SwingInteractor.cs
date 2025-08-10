using UnityEngine;
using UnityEngine.UI;

public class SwingInteractor : MonoBehaviour
{
    [SerializeField] private Button _pushButton;
    [SerializeField] private SwingPusher _pusher;
    
    private void OnEnable()
    {
        _pushButton.onClick.AddListener(OnPushButtonClicked);
    }
    
    private void OnDisable()
    {
        _pushButton.onClick.RemoveListener(OnPushButtonClicked);
    }
    
    private void OnPushButtonClicked()
    {
        _pusher.Push();
    }
}
