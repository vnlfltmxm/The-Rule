using Script.Util;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundSettingUI : MonoBehaviour
{
    [SerializeField] private Toggle _soundToggle; // 사운드 on/off
    [SerializeField] private Slider _musicVolumeSlider; // 음악 볼륨
    [SerializeField] private Slider _sfxVolumeSlider; // 효과음 볼륨
    

    private void Start()
    {
        InitializeGameSettings();
    }

    private void InitializeGameSettings()
    {
        // 사운드 설정
        _soundToggle.isOn = AudioManager.Instance.IsSoundEnabled;

        // 볼륨 설정 (가정: AudioManager에서 관리)
        _musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        _sfxVolumeSlider.value = AudioManager.Instance.SFXVolume;
    }

    public void ApplyGameSettings()
    {
        // 사운드 설정 적용
        AudioManager.Instance.SetSoundEnabled(_soundToggle.isOn);

        // 볼륨 설정 적용
        AudioManager.Instance.SetMusicVolume(_musicVolumeSlider.value);
        AudioManager.Instance.SetSFXVolume(_sfxVolumeSlider.value);
    }

    public void ResetGameSettings()
    {
        // 기본값으로 복원
        _soundToggle.isOn = true; // 기본 사운드 켬
        _musicVolumeSlider.value = 0.5f; // 기본 음악 볼륨 50%
        _sfxVolumeSlider.value = 0.5f; // 기본 효과음 볼륨 50%
    }
}
