using Script.Pawn.Player;
using UnityEngine;

public class ChildMonsterCry : ChildMonsterState, IObjectState<ChildMonsterCry>
{
    private float _intervalTime = 1f;
    private float _currentTime;
    private int _damage = 1;
    
    private Player _player;
    
    public ChildMonsterCry(ChildMonster childMonster) : base(childMonster)
    {
    }
    
    public void StateEnter()
    {
        _player = Player.S_Player;
        _currentTime = 0f;
    }

    public void StateUpdate()
    {
        if (_player == null)
            return;
        
        _currentTime += Time.deltaTime;
        if (_intervalTime < _currentTime)
        {
            _currentTime -= _intervalTime;
            _player.PlayerBodyCondition.OnDamage(_damage);
        }
    }

    public void StateExit()
    {
    }
}
