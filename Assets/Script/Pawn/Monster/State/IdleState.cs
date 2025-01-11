using System;
using LN;
using Script.Pawn;
using UnityEngine;

public class IdleState : State<Pawn>
{
    private float _waitTime;
    private float _startTime;
    private float _currentTime;
    
    public IdleState(Pawn stateObject) : base(stateObject)
    {
        //내용
    }
    
    public override void StateEnter()
    {
        _startTime = Time.time;
    }
    public override Type StateCheck()
    {
        _currentTime = Time.time;
        return _currentTime - _startTime > _waitTime ? typeof(PatrolState) : null;
    }
    public override void StateExit()
    {
        _currentTime = 0f;
        _startTime = 0f;
    }
}
