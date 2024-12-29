using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Slider _frameRateSlider;
    
    private void Start()
    {
        InitializeUI();
    }
    private void InitializeUI()
    {
        // 해상도 설정
        _resolutionDropdown.ClearOptions();
        var resolutions = Screen.resolutions;
        var resolutionOptions = new System.Collections.Generic.List<string>();
        foreach (var res in resolutions)
        {
            resolutionOptions.Add(res.width + "x" + res.height);
        }
        _resolutionDropdown.AddOptions(resolutionOptions);
        _resolutionDropdown.value = resolutionOptions.IndexOf(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
        
        // 품질 설정
        _qualityDropdown.ClearOptions();
        var qualityOptions = new System.Collections.Generic.List<string>();
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            qualityOptions.Add(QualitySettings.names[i]);
        }
        _qualityDropdown.AddOptions(qualityOptions);
        _qualityDropdown.value = QualitySettings.GetQualityLevel();

        // 전체 화면 설정
        _fullscreenToggle.isOn = Screen.fullScreen;

        // V-Sync 설정
        _vsyncToggle.isOn = QualitySettings.vSyncCount > 0;

        // 프레임 제한 설정
        _frameRateSlider.minValue = 30;
        _frameRateSlider.maxValue = 144;
        _frameRateSlider.value = Application.targetFrameRate;
    }
    public void ApplySettings()
    {
        // 해상도 적용
        var selectedResolution = Screen.resolutions[_resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, _fullscreenToggle.isOn);

        // 품질 설정 적용
        QualitySettings.SetQualityLevel(_qualityDropdown.value);

        // V-Sync 설정 적용
        QualitySettings.vSyncCount = _vsyncToggle.isOn ? 1 : 0;

        // 프레임 제한 설정 적용
        Application.targetFrameRate = (int)_frameRateSlider.value;
    }
    public void ResetToDefault()
    {
        // 기본값으로 복원
        _resolutionDropdown.value = _resolutionDropdown.options.FindIndex(option => option.text == Screen.currentResolution.width + "x" + Screen.currentResolution.height);
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _fullscreenToggle.isOn = Screen.fullScreen;
        _vsyncToggle.isOn = QualitySettings.vSyncCount > 0;
        _frameRateSlider.value = Application.targetFrameRate;
    }
}
