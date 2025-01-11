using System;
using LN;
using Script.Manager.Framework;
using Script.Pawn;
using UnityEngine;

public class LookState : State<Pawn>
{
    public InvadeHead _head;
    private Transform _target;
    
    private float _viewAngle;
    private float _viewDistance;
    private Vector3 _offSet = new Vector3(0f, 1f, 1f);
    private LayerMask _targetLayer = LayerMask.GetMask("Player");
    
    public LookState(Pawn stateObject) : base(stateObject)
    {
        _head = _stateObject.GetComponentInChildren<InvadeHead>();
    }
    public override void StateEnter()
    {
        _head.StartHeadRotation();
    }
    public override Type StateCheck()
    {
        if (_target == null || _head.Completed)
            return typeof(PatrolState);
        return null;
    }
    public override void StateUpdate()
    {
        if(DetectionUtils.CalculateAngle(_head.transform, _target, _viewAngle) && DetectionUtils.CalculateDistance(_head.transform, _target, _viewAngle))
        {
            DetectionUtils.RayCast(_head.transform, _target, _viewDistance, _offSet, _targetLayer);
        }
    }
    public override void StateExit()
    {
        _head.StopHeadRotation();

        _head.Completed = false;
    }

    public void SetPlayer(Transform player)
    {
        _target = player;
    }
}
