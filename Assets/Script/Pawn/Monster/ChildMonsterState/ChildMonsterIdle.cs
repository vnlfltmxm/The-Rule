using UnityEngine;

public class ChildMonsterIdle : ChildMonsterState, IObjectState<ChildMonsterIdle>
{
    private float _waitTime;
    private float _currentTime;
    
    public ChildMonsterIdle(ChildMonster childMonster) : base(childMonster)
    {
        _waitTime = childMonster.WaitTime;
    }
    
    public void StateEnter()
    {
        _currentTime = 0f;
    }

    public void StateUpdate()
    {
        _currentTime += Time.deltaTime;
        if (_waitTime < _currentTime)
            state.ChangeObjectState(ChildState.Cry);
    }

    public void StateExit()
    {
    }
}
