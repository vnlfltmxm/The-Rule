using Script.Util;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class GameSettingUI : MonoBehaviour
{
    //DropdownMenu
    [SerializeField] private TMP_Dropdown _difficultyDropdown; // 난이도 설정
    [SerializeField] private Toggle _autoSaveToggle; // 자동 저장
    
    private void Start()
    {
        InitializeGameSettings();
    }
    private void InitializeGameSettings()
    {
        // 난이도 설정
        _difficultyDropdown.ClearOptions();
        _difficultyDropdown.AddOptions(new System.Collections.Generic.List<string> { "Easy", "Normal", "Hard" });
        _difficultyDropdown.value = (int)GameManager.Instance.GameDifficulty;

        // 자동 저장 설정
        _autoSaveToggle.isOn = GameManager.Instance.IsAutoSaveEnabled;
    }
    public void ApplyGameSettings()
    {
        // 난이도 설정 적용
        GameManager.Instance.SetGameDifficulty((GameDifficulty)_difficultyDropdown.value);

        // 자동 저장 설정 적용
        GameManager.Instance.SetAutoSave(_autoSaveToggle.isOn);
    }
    public void ResetGameSettings()
    {
        _difficultyDropdown.value = 1; // 기본 난이도 Normal
        _autoSaveToggle.isOn = true; // 기본 자동 저장 켬
    }
}
