using Script.Pawn.Player;
using Script.Util;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameDifficulty GameDifficulty = GameDifficulty.Easy;
    public bool IsAutoSaveEnabled = true;

    protected override void Init()
    {
        
    }

    public void SetGameDifficulty(GameDifficulty difficulty)
    {
        GameDifficulty = difficulty;
    }

    public void SetAutoSave(bool isEnabled)
    {
        IsAutoSaveEnabled = isEnabled;
    }
}
