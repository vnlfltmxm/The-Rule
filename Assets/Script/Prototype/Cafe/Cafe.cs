using UnityEngine;

public class Cafe : MonoBehaviour, IInteractionCafe
{
    [Header("PlayerPosition")]
    [SerializeField] private Transform _cafePlayerPosition;
    [Header("CafeCamera")]
    [SerializeField] private GameObject _cafeCamera;

    private CafeUIController _cafeUIController;
    private TestPlayerInputSystem _testPlayerInputSystem;
    private GameObject _cafePlayer;

    private void Awake()
    {
        InitializeCafe();
    }

    private void InitializeCafe()
    {
        _cafeUIController = transform.GetComponentInChildren<CafeUIController>();

        if (_cafeUIController != null)
        {
            _cafeUIController.UnLockPlayer = UnLockPlayer;

            _cafeUIController.SetActiveCamera = SetActiveCamera;
        }
    }

    public void Interaction(GameObject playerObject)
    {
        _cafePlayer = playerObject;

        _testPlayerInputSystem = playerObject.GetComponent<TestPlayerInputSystem>();

        if(_testPlayerInputSystem == null)
        {
            return;
        }

        _testPlayerInputSystem.PlayerLock(true);
        
        _cafeUIController.OnActiveMenuUI(MovePlayer);
    }

    public void MovePlayer()
    {
        _cafePlayer.transform.position = _cafePlayerPosition.position;

        SetActiveCamera(true);
    }

    public void UnLockPlayer()
    {
        SetActiveCamera(false);

        _testPlayerInputSystem.PlayerLock(false);
    }

    public void SetActiveCamera(bool isActive)
    {
        _cafeCamera.SetActive(isActive);
    }
}
