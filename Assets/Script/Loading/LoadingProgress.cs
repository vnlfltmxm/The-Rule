using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider progressSlider;
    private void Start()
    {
        SceneLoadManager.OnLoadingProgress = OnChangeProgress;
    }
    private void OnChangeProgress(float progress)
    {
        progressText.text = $"{progress * 100} %";
        progressSlider.value = progress;
    }
}
