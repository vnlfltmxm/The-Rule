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

    [Header("DialogData")]
    [SerializeField] private CafeDialog _dialog;

    [Header("SelectUI")]
    [SerializeField] private CafeSelectUI _selectUI;

    private Action _unlockPlayer;
    private Action<bool> _setActiveCamera;

    public void SetAction(Action unlockPlayer, Action<bool> setActiveCamera)
    {
        _unlockPlayer = unlockPlayer;
        _setActiveCamera = setActiveCamera;
    }

    public void OnActiveMenuUI(Action<GameObject> movePlayer, GameObject player)
    {
        StartCoroutine(OnMenuUI(movePlayer, player));
    }

    private IEnumerator OnMenuUI(Action<GameObject> movePlayer, GameObject player)
    {
        StartCoroutine(_backGroundController.FadeBackGround());

        yield return WaitForAlpha(1f);

        movePlayer?.Invoke(player);

        yield return WaitForAlpha(0f);

        yield return StartCoroutine(_bannerUI.StartClerkDialog(_dialog.OnMenuUI));
        
        _menuUI.SetActive(true);
    }

    public void OnClickCancel()
    {
        CafeManager.Instance.ResetCafeMenu();
    }

    private IEnumerator WaitForAlpha(float targetAlpha)
    {
        yield return new WaitUntil(() =>
        {
            return Mathf.Approximately(_backGroundController.BackGroundImage.color.a, targetAlpha);
        }); //Mathf.Approximately -> 부동 소수점값 2개가 거의 같은지 확인. 거의 같다면 true, 벗어난다면 false 리턴
    }

    public void OnClickExitButton()
    {
        //이 부분에 역무원이 현재 플레이어를 계속 추적하도록 하는 메서드 호출.
        CafeManager.Instance.ResetCafeMenu();

        var player = CafeManager.Instance.PlayerPrefab;

        var stationWorkerList = EnemyManager.Instance.StationWorkerList;

        foreach(var stationWorker in stationWorkerList)
        {
            if(stationWorker != null)
            {
                stationWorker.OnSystemTracking(player.transform);
            }
        }

        _unlockPlayer?.Invoke();

        _setActiveCamera?.Invoke(false);

        _menuUI.SetActive(false);
    }

    public void OnClickBuyButton()
    {
        CafeManager.Instance.ResetCafeMenu();

        _menuUI.SetActive(false);

        StartCoroutine(OnSelectUI());
    }

    private IEnumerator OnSelectUI()
    {
        yield return StartCoroutine(_bannerUI.StartClerkDialog(_dialog.SelectUI));

        _selectUI.gameObject.SetActive(true);
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
