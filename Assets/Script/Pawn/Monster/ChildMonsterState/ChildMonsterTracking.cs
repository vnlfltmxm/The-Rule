using Script.Pawn.Player;
using UnityEngine;
using UnityEngine.AI;

public class ChildMonsterTracking : ChildMonsterState, IObjectState<ChildMonsterTracking>
{
    private Player _player;
    private NavMeshAgent _agent;
    
    public ChildMonsterTracking(ChildMonster childMonster) : base(childMonster)
    {
        _agent = base.childMonster.GetComponent<NavMeshAgent>();
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
    }

    public void StateExit()
    {
        _agent.enabled = false;
    }
}
