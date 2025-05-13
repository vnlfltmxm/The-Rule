using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace DeathCafe
{
    public class BackGroundUI : MonoBehaviour
    {
        [Header("BackGroundImage"), SerializeField] private Image _backImage;
        [Header("DurationTime"), SerializeField] private float _durationTime;

        public void StartFadeEffect(Action callBack, Action onAlphaComplete)
        {
            StartCoroutine(FadeEffect(callBack, onAlphaComplete));
        }

        private IEnumerator FadeEffect(Action callBack, Action onAlphaComplete)
        {
            EnableImage();

            yield return StartCoroutine(AlphaControl(1f));
            onAlphaComplete.Invoke();
            yield return StartCoroutine(AlphaControl(0f));

            EnableImage();
            callBack.Invoke();
        }

        private IEnumerator AlphaControl(float targetAlpha)
        {
            var currentColor = _backImage.color;
            var startAlpha = currentColor.a;
            var elapsed = 0f;

            while(elapsed < _durationTime)
            {
                elapsed += Time.deltaTime;
                currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsed / _durationTime);
                _backImage.color = currentColor;

                yield return null;
            }

            currentColor.a = targetAlpha;
            _backImage.color = currentColor;
        }

        private void EnableImage()
        {
            bool activeSelf = _backImage.gameObject.activeSelf ? false : true;
            _backImage.gameObject.SetActive(activeSelf);
        }
    }
}

