using Script.Util;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameDifficulty GameDifficulty = GameDifficulty.Easy;
    public bool IsAutoSaveEnabled = true;

    public void SetGameDifficulty(GameDifficulty difficulty)
    {
        GameDifficulty = difficulty;
    }

    public void SetAutoSave(bool isEnabled)
    {
        IsAutoSaveEnabled = isEnabled;
    }
}
