using System.Collections;
using UnityEngine;

public class CafeUIController : MonoBehaviour
{
    [Header("MenuUI")]
    [SerializeField] private GameObject _menuUI;

    private BannerUI _bannerUI;
    private CafeBackGroundController _backGroundController;

    public Transform CafePosition { get; set; }

    private void Awake()
    {
        InitializeCafeUIController();
    }

    private void InitializeCafeUIController()
    {
        _bannerUI = transform.GetComponentInChildren<BannerUI>();
        _backGroundController = transform.GetComponentInChildren<CafeBackGroundController>();
    }

    public void OnActiveMenuUI(GameObject playerObject, GameObject cafeCamera)
    {
        StartCoroutine(OnMenuUI(playerObject, cafeCamera));
    }

    private IEnumerator OnMenuUI(GameObject playerObject, GameObject cafeCamera)
    {
        var testPlayerInput = playerObject.GetComponent<TestPlayerInputSystem>();

        if(testPlayerInput != null)
        {
            testPlayerInput.PlayerLock(true);
        }

        StartCoroutine(_backGroundController.FadeBackGround());

        yield return WaitForAlpha(1f);

        playerObject.transform.position = CafePosition.position;

        cafeCamera.SetActive(true);

        yield return WaitForAlpha(0f);

        yield return StartCoroutine(_bannerUI.StartClerkDialog());

        //메뉴UI On
    }

    private IEnumerator WaitForAlpha(float targetAlpha)
    {
        yield return new WaitUntil(() =>
        {
            return Mathf.Approximately(_backGroundController.BackGroundImage.color.a, targetAlpha);
        }); //Mathf.Approximately -> 부동 소수점값 2개가 거의 같은지 확인. 거의 같다면 true, 벗어난다면 false 리턴
    }
}
