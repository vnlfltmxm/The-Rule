using Script.Pawn;
using Script.Util;
using UnityEngine;

public class AttackState : StateElement
{
    public override StateType StateType => StateType.Attack;
    private Pawn _targetPawn;
    private float _intervalTime;
    private float _elapsedTime;
    private float _damage;
    
    //private GameObjectField _targetPawn;
    //private FloatField _intervalTime;
    //private FloatField _elapsedTime;
    //private FloatField _damage;
    
    public AttackState(Pawn owner, StateElementData data) : base(owner, data)
    {
        _targetPawn = owner.Target;
        _intervalTime = data.IntervalTime;
        _damage = data.Damage;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        _elapsedTime = 0f;
    }
    public override void Stay()
    {
        _elapsedTime += Time.deltaTime;
        if(_intervalTime < _elapsedTime)
            return;
        _targetPawn.OnDamage(_damage);
    }
    public override void Exit()
    {
    }
}
