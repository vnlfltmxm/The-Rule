using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CafeBackGroundController : MonoBehaviour
{
    [Header("FadeDurationTime")]
    [SerializeField] private float _duractionTime;

    private Image _backGroundImage;
    public Image BackGroundImage => _backGroundImage;

    
    private void Awake()
    {
        _backGroundImage = transform.GetComponentInChildren<Image>(true);
    }

    public IEnumerator FadeBackGround()
    {
        ActiveImage(true);

        yield return StartCoroutine(SetAlpha(_backGroundImage.color, 1f));

        yield return StartCoroutine(SetAlpha(_backGroundImage.color, 0f));

        ActiveImage(false);
    }

    private IEnumerator SetAlpha(Color backGroundColor, float targetAlpha)
    {
        float startAlpha = backGroundColor.a;

        float elapsed = 0f; //경과시간

        while (elapsed < _duractionTime) //경과시간이 총 시간보다 작다면 동작.
        {
            elapsed += Time.deltaTime; //코루틴이 실행된 총 경과시간.

            backGroundColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsed / _duractionTime); //경과시간 / 총 시간 = 진행도(0 ~ 1사이의 값) 
                                                                                               //StartAlpha에서 targetAlpha까지 진행도 만큼 부드럽게 변화.  
            _backGroundImage.color = backGroundColor;

            yield return null;
        }

        backGroundColor.a = targetAlpha;

        _backGroundImage.color = backGroundColor;
    }

    private void ActiveImage(bool isActive)
    {
        _backGroundImage.gameObject.SetActive(isActive);
    }
}
