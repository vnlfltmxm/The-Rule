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
        //리스폰 된 후 _waitTime만큼 미아가 리스폰되지 않을시
        //비명을 지른 후 플레이어를 추격함
        _currentTime += Time.deltaTime;
        if (_waitTime < _currentTime)
            state.ChangeObjectState(MomState.Tracking);
    }

    public void StateExit()
    {
    }
}
