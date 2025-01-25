using UnityEngine;

public class MomMonsterIdle : MomMonsterState, IObjectState<MomMonsterIdle>
{
    private float _waitTime;
    private float _currentTime;
    
    public MomMonsterIdle(MomMonster momMonster) : base(momMonster)
    {
        _waitTime = momMonster.WaitTime;
    }
    
    public void StateEnter()
    {
        _currentTime = 0f;
    }
    
    public void StateUpdate()
    {
        _currentTime += Time.deltaTime;
        if (_waitTime < _currentTime)
            state.ChangeObjectState(MomState.Tracking);
    }

    public void StateExit()
    {
    }
}
