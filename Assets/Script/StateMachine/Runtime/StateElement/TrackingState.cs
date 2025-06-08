using System;
using Script.Pawn;
using UnityEngine.AI;

[Serializable, StateElementType(typeof(AttackState))]
public class TrackingState : StateElement
{
    private NavMeshAgent _agent;
    private Pawn _targetPawn;
    
    /*public TrackingState(StateMachine stateMachine) : base(stateMachine)
    {
        //_agent = owner.GetComponent<NavMeshAgent>();
        //_targetPawn = owner.Target;
        //_agent.speed = data.TrackingSpeed;
        //_agent.enabled = true;
    }*/

    public override void Enter()
    {
        base.Enter();
        //_targetPawn = StateObject.Target;
    }
    public override void Stay()
    {
        //_agent.enabled = true;
        _agent.SetDestination(_targetPawn.transform.position);
    }
    public override void Exit()
    {
    }
}
