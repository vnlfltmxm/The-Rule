using System;
using Script.Pawn;
using UnityEngine.AI;

[Serializable, StateElementType(typeof(AttackState))]
public class PatrolState : StateElement
{
    private NavMeshAgent _agent;
    
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