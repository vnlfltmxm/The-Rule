using System;
using Script.Pawn;
using Script.Pawn.Player;
using Script.Util;
using UnityEngine;
using UnityEngine.AI;

public class TrackingState : StateElement
{
    public override StateType StateType => StateType.Tracking;
    private NavMeshAgent _agent;
    private Pawn _targetPawn;
    
    public TrackingState(Pawn owner, TrackingStateData data) : base(owner, data)
    {
        _agent = owner.GetComponent<NavMeshAgent>();
        _targetPawn = owner.Target;
        _agent.speed = data.TrackingSpeed;
        _agent.enabled = true;
    }

    public override void Enter()
    {
        base.Enter();
        _targetPawn = StateObject.Target;
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
