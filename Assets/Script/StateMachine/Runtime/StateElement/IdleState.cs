using System;
using Script.Pawn;

[Serializable, StateElementType(typeof(AttackState))]
public class IdleState : StateElement
{
    
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