using Script.Util;
using UnityEngine;

public class PlayerBodyCondition
{
    private int MaxHp = 100;
    private int _hp = 100;
    public int Hp => _hp;

    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
    
    private float _sprintSpeed;
    public float SprintSpeed => _sprintSpeed;

    public void Init()
    {
        DebugManager.Instance.SetDebugData(DebugType.PlayerHP, $"{MaxHp} / {_hp}");
    }

    public void OnDamage(int damage)
    {
        _hp -= damage;
        DebugManager.Instance.SetDebugData(DebugType.PlayerHP, $"{MaxHp} / {_hp}");
        if (_hp < 0)
            OnDie();
    }

    public void OnDie()
    {
        Logger.LogWarning("플레이어 주금", Color.blue);
    }
}
