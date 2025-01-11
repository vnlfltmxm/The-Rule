using System;
using LN;
using Script.Pawn;
using UnityEngine;

public class IdleState : State
{
    private float _waitTime;
    private float _startTime;
    private float _currentTime;
    
    public override void InitState(Pawn stateObject)
    {
        base.InitState(stateObject);
        if(_stateObject is MonsterBase monsterBase)
            _waitTime = monsterBase.WaitTime;
    }
    public override void StateEnter()
    {
        _startTime = Time.time;
    }

    public override void StateUpdate()
    {
        _currentTime = Time.time;
    }

    public override Type StateCheck()
    {
        return _currentTime - _startTime > _waitTime ? typeof(PatrolState) : null;
    }
    public override void StateExit()
    {
        _currentTime = 0f;
        _startTime = 0f;
    }
}
