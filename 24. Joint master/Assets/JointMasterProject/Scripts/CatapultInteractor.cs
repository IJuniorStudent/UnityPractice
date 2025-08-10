using UnityEngine;
using UnityEngine.UI;

public class CatapultInteractor : MonoBehaviour
{
    [SerializeField] private Button _prepareButton;
    [SerializeField] private Button _fireButton;
    [SerializeField] private Catapult _catapult;
    
    private void OnEnable()
    {
        _prepareButton.onClick.AddListener(OnPrepareButtonClicked);
        _fireButton.onClick.AddListener(OnFireButtonClicked);
    }
    
    private void OnDisable()
    {
        _prepareButton.onClick.RemoveListener(OnPrepareButtonClicked);
        _fireButton.onClick.RemoveListener(OnFireButtonClicked);
    }
    
    private void OnPrepareButtonClicked()
    {
        _catapult.PrepareShot();
    }
    
    private void OnFireButtonClicked()
    {
        _catapult.Fire();
    }
}
