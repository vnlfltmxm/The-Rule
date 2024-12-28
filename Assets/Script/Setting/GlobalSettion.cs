using UnityEngine;

public static class GlobalSettion
{
    public static GameSetting GameSetting = new GameSetting();
    public static VideoSetting VideoSetting = new VideoSetting();
    public static SoundSetting SoundSetting = new SoundSetting();
    public static InputSetting InputSetting = new InputSetting();
    
    
    public static void ApplySettings()
    {
        /*// 해상도 적용
        var selectedResolution = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenToggle.isOn);

        // 품질 설정 적용
        QualitySettings.SetQualityLevel(qualityDropdown.value);

        // V-Sync 설정 적용
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;

        // 프레임 제한 설정 적용
        if (int.TryParse(frameRateInput.text, out int frameRate))
        {
            Application.targetFrameRate = frameRate;
        }*/
    }
    
}
