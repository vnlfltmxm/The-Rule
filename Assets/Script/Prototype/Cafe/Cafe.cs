using Script.Pawn.Player;
using UnityEngine;
using Script.Prop;

public class Cafe : MonoBehaviour, IInteractable
{
    [Header("PlayerPosition")]
    [SerializeField] private Transform _cafePlayerPosition;
    [Header("CafeCamera")]
    [SerializeField] private GameObject _cafeCamera;
    [Header("CafeUIController"), SerializeField]
    private CafeUIController _cafeUIController;

    public bool IsInteractable { get; set; } = true;

    public void MovePlayer(GameObject playerObject)
    {
        playerObject.transform.position = _cafePlayerPosition.position;

        SetActiveCamera(true);
    }

    public void UnLockPlayer(Player player)
    {
        SetActiveCamera(false);

        player.ControlLock(false);
    }

    public void SetActiveCamera(bool isActive)
    {
        _cafeCamera.SetActive(isActive);
    }

    
    public void OnInteract(IInteractor sender)
    {
        CafeManager.Instance.PlayerPrefab = sender.GameObject;
        
        Player player =
            sender.GameObject.GetComponent<Player>();

        if(player != null)
        {
            player.ControlLock(true);

            _cafeUIController.SetAction(() => UnLockPlayer(player),
                SetActiveCamera);
            _cafeUIController.OnActiveMenuUI(MovePlayer, sender.GameObject);

            CafeManager.Instance.SetUnlockPlayerAction(() => UnLockPlayer(player),
                () => SetActiveCamera(false));
        }

        _cafeUIController.OnActiveMenuUI(MovePlayer, sender.GameObject);
    }
}
