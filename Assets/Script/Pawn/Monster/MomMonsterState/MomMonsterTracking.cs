using Script.Pawn.Player;
using UnityEngine;
using UnityEngine.AI;

public class MomMonsterTracking : MomMonsterState, IObjectState<MomMonsterTracking>
{
    private Player _player;
    private NavMeshAgent _agent;
    
    public MomMonsterTracking(MomMonster momMonster) : base(momMonster)
    {
        _agent = base.momMonster.GetComponent<NavMeshAgent>();
        _agent.enabled = true;
    }
    
    public void StateEnter()
    {
        _player = Player.S_Player;
    }

    public void StateUpdate()
    {
        if (_player == null)
        {
            Logger.Log("플레이어가 존재하지 않음");
            return;
        }
        
        Vector3 playerPosition = _player.transform.position;
        _agent.SetDestination(playerPosition);

        if (Vector3.Distance(playerPosition, momMonster.transform.position) < 1f)
            _player.PlayerBodyCondition.OnDie();
    }

    public void StateExit()
    {
        _agent.enabled = false;
    }
}
