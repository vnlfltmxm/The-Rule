using UnityEngine;
using Script.Prop;

public class Cafe : MonoBehaviour, IInteractableCafe
{
    [Header("PlayerPosition")]
    [SerializeField] private Transform _cafePlayerPosition;
    [Header("CafeCamera")]
    [SerializeField] private GameObject _cafeCamera;

    private CafeUIController _cafeUIController;

    public bool IsInteractable { get; set; } = true;

    public void MovePlayer(GameObject playerObject)
    {
        playerObject.transform.position = _cafePlayerPosition.position;

        SetActiveCamera(true);
    }

    public void UnLockPlayer(PlayerUtilityController controller)
    {
        SetActiveCamera(false);

        controller.PlayerLock(false);
    }

    public void SetActiveCamera(bool isActive)
    {
        _cafeCamera.SetActive(isActive);
    }

    public void InteractObject(GameObject playerObject)
    {
        CafeManager.Instance.PlayerPrefab = playerObject;

        PlayerUtilityController playerUtilityController =
            playerObject.GetComponent<PlayerUtilityController>();

        if(playerUtilityController != null)
        {
            playerUtilityController.PlayerLock(true);

            _cafeUIController.SetAction(() => UnLockPlayer(playerUtilityController),
                SetActiveCamera);
            _cafeUIController.OnActiveMenuUI(MovePlayer, playerObject);

            CafeManager.Instance.SetUnlockPlayerAction(() => UnLockPlayer(playerUtilityController),
                () => SetActiveCamera(false));
        }
    }
}
