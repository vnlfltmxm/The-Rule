using System.Collections;
using UnityEngine;

public class InvadeObjectIdle : InvadeObjectState, IObjectState<InvadeObjectIdle>
{
    public InvadeObjectIdle(InvadeObject invadeObject) : base(invadeObject)
    {
        _waitTime = invadeObject.WaitTime;
    }

    private float _waitTime;
    private float _startTime;
    private float _currentTime;

    public void StateEnter()
    {
        _startTime = Time.time;
    }

    public void StateUpdate()
    {
        _currentTime = Time.time;

        if(_currentTime - _startTime > _waitTime)
        {
            _state.ChangeObjectState(InvadeState.Patrol);
        }
    }

    public void StateExit()
    {
        _currentTime = 0f;
        _startTime = 0f;
    }
}
