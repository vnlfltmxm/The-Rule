using System;
using Script.Pawn;
using UnityEngine;

[Serializable, StateElementType(typeof(AttackState))]
public class AttackState : StateElement
{
    private GameObjectField _targetPawn;
    private FloatField _intervalTime;
    private FloatField _damage;
    
    private float _elapsedTime;
    
    public override void Enter()
    {
        base.Enter();
        
        _elapsedTime = 0f;
    }
    public override void Stay()
    {
        _elapsedTime += Time.deltaTime;
        if(_intervalTime.Value < _elapsedTime)
            return;
        _targetPawn.Value.GetComponent<Pawn>().OnDamage(_damage.Value);
    }
    public override void Exit()
    {
    }
}
