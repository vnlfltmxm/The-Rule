using System;
using Script.Pawn;
using Script.Util;
using UnityEngine;

public class IdleState : StateElement
{
    public override StateType StateType => StateType.Idle;
    public IdleState(Pawn t, IdleStateData data) : base(t, data)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }
    public override void Stay()
    {
    }
    public override void Exit()
    {
    }
}