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

    public enum AreaType
    {
        Null = 0,
        Shop = 1 << 3,
        Area1 = 1 << 4,
        Area2 = 1 << 5,
        Area3 = 1 << 6,
    }

    public enum SpecialSpawnType
    {
        SpawnPos,
        PlayerBack,
    }

    public enum SpawnEventType
    {
        Null,
        PlayerLookSpawned,  //플레이어가 스폰 오브젝트를 바라보는 이벤트
    }

    public enum HierarchyParentType
    {
        Manager,
        PawnParent,
        
    }

    public enum DebugType
    {
        PlayerHP,
        PlayerPosition,
        PlayerRotation,
        PlayerVelocity,
        PlayerCurrentArea,
    }
}