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

    public enum Condition
    {
        Null,
        ObjectSpawned,
    }

    [Flags]
    public enum AreaType
    {
        Null = 0,
        Shop = 1 << 0,
        Area1 = 1 << 1,
        Area2 = 1 << 2,
        Area3 = 1 << 3,
    }
}