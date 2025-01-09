using UnityEngine;

public class Cafe : MonoBehaviour, IInteractionCafe
{
    [Header("PlayerPosition")]
    [SerializeField] private Transform _cafePlayerPosition;
    [Header("CafeCamera")]
    [SerializeField] private GameObject _cafeCamera;

    private CafeUIController _cafeUIController;

    private void Awake()
    {
        SetCafeUIController();
    }

    private void SetCafeUIController()
    {
        _cafeUIController = transform.GetComponentInChildren<CafeUIController>();

        if(_cafeUIController != null )
        {
            _cafeUIController.CafePosition = _cafePlayerPosition;
        }
    }

    public void Interaction(GameObject playerObject)
    {
        _cafeUIController.OnActiveMenuUI(playerObject, _cafeCamera);
    }
}
