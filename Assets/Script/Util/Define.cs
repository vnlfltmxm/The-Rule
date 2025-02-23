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
        Null,
        Shop,
        Plane1,
        Plane2,
        Plane3,
    }
}