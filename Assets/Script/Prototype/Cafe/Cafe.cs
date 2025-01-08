using UnityEngine;
using UnityEngine.InputSystem;

public class Cafe : MonoBehaviour, IInteractionCafe
{
    [Header("PlayerPosition")]
    [SerializeField] private Transform _playerPosition;
    [Header("CafeCamera")]
    [SerializeField] private GameObject _cafeCamera;

    private CafeUI _cafeUI;

    private void Awake()
    {
        _cafeUI = transform.GetComponentInChildren<CafeUI>();
    }

    public void Interaction(GameObject playerObject)
    {
        SetCafeCamera(true);

        SetPlayer(playerObject);

        _cafeUI.OnActiveMenuUI();
    }

    private void SetCafeCamera(bool isActive)
    {
        _cafeCamera.SetActive(isActive);
    }

    private void SetPlayer(GameObject playerObject)
    {
        playerObject.transform.position = _playerPosition.position;

        var playerInput = playerObject.GetComponent<PlayerInput>();

        if(playerInput != null)
        {
            playerInput.enabled = false;
        }
    }
}
