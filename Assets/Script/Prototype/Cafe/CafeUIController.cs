using System;
using System.Collections;
using UnityEngine;

public class CafeUIController : MonoBehaviour
{
    [Header("MenuUI")]
    [SerializeField] private GameObject _menuUI;

    [Header("BannerUI")]
    [SerializeField] private BannerUI _bannerUI;

    [Header("BackGroundController")]
    [SerializeField] private CafeBackGroundController _backGroundController;

    public Action UnLockPlayer { get; set; }
    public Action<bool> SetActiveCamera { get; set; }

    public void OnActiveMenuUI(Action movePlayer)
    {
        StartCoroutine(OnMenuUI(movePlayer));
    }

    private IEnumerator OnMenuUI(Action movePlayer)
    {
        StartCoroutine(_backGroundController.FadeBackGround());

        yield return WaitForAlpha(1f);

        movePlayer?.Invoke();

        yield return WaitForAlpha(0f);

        yield return StartCoroutine(_bannerUI.StartClerkDialog());

        _menuUI.SetActive(true);
    }

    public void Exit()
    {
        CafeManager.Instance.ResetCafeMenu();

        UnLockPlayer?.Invoke();

        SetActiveCamera?.Invoke(false);

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
