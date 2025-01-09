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

        float elapsed = 0f;

        while (elapsed < _duractionTime)
        {
            elapsed += Time.deltaTime;

            backGroundColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsed / _duractionTime);

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
