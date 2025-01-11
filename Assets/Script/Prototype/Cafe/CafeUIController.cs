using System;
using System.Collections;
using UnityEngine;

public class CafeUIController : MonoBehaviour
{
    [Header("MenuUI")]
    [SerializeField] private GameObject _menuUI;

    private BannerUI _bannerUI;
    private CafeBackGroundController _backGroundController;

    private GameObject _cafeCamera;
    private TestPlayerInputSystem _playerInput;

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
        _playerInput = playerObject.GetComponent<TestPlayerInputSystem>();
        _cafeCamera = cafeCamera;

        if (_playerInput != null)
        {
            _playerInput.PlayerLock(true);
        }

        StartCoroutine(_backGroundController.FadeBackGround());

        yield return WaitForAlpha(1f);

        playerObject.transform.position = CafePosition.position;

        _cafeCamera.SetActive(true);

        yield return WaitForAlpha(0f);

        yield return StartCoroutine(_bannerUI.StartClerkDialog());

        _menuUI.SetActive(true);
    }

    public void Exit()
    {
        CafeManager.Instance.ResetCafeMenu();

        _playerInput.PlayerLock(false);

        _cafeCamera.SetActive(false);

        _menuUI.SetActive(false);
    }

    private IEnumerator WaitForAlpha(float targetAlpha)
    {
        yield return new WaitUntil(() =>
        {
            return Mathf.Approximately(_backGroundController.BackGroundImage.color.a, targetAlpha);
        }); //Mathf.Approximately -> 부동 소수점값 2개가 거의 같은지 확인. 거의 같다면 true, 벗어난다면 false 리턴
    }

    #region Test
    //private void Test()
    //{
    //    StartCoroutine(WaitForAlpha(1f, () => isReady));
    //}

    //private IEnumerator WaitForAlpha(float targetAlpha, Func<bool> func)
    //{
    //    yield return new WaitUntil(() =>
    //    {
    //        return Mathf.Approximately(_backGroundController.BackGroundImage.color.a, targetAlpha) && func();
    //    });
    //}
    #endregion
}
