using System;

namespace Script.Util
{
    public enum InputType
    {
        Move,
        Sprint,
        Crouch,
        Interact,
        PrimaryAction,
        SecondaryAction,
        CheckRules,
        Look,
    }

    public enum GameDifficulty
    {
        Easy, 
        Normal, 
        Hard
    }

    [Flags]
    public enum Condition
    {
        Null,
        ObjectSpawned,
    }
}