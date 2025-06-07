using Script.Pawn;
using Script.Util;
using UnityEngine.AI;

public class PatrolState : StateElement
{
    public override StateType StateType => StateType.Patrol;
    private NavMeshAgent _agent;
    public PatrolState(Pawn owner, PatrolStateData data) : base(owner, data)
    {
        _agent = owner.GetComponent<NavMeshAgent>();
        _agent.speed = data.PatrolSpeed;
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