using System;
using System.Collections.Generic;
using LN;
using Script.Manager.Framework;
using Script.Pawn;
using UnityEngine;
using UnityEngine.AI;

public class SoundTraceState : State<Pawn>
{
    private Transform _player;
    
    protected NavMeshPath _path;
    protected NavMeshAgent _agent;
    protected Rigidbody _rigidbody;
    protected List<Vector3> _pathList;
    protected Vector3 _currentPath;

    protected float _rotationSpeed;
    protected float _movementSpeed;
    protected int _pathListIndex;
    
    protected float _viewDistance;
    protected float _viewAngle;

    protected Vector3 _offSet = new Vector3(0f, 1f, 1f);
    
    private LayerMask _targetLayer = LayerMask.GetMask("Player", "Wall");
    
    public SoundTraceState(Pawn stateObject) : base(stateObject)
    {
        //내용
    }
    public override void StateEnter()
    {
        CalculateTracePath();
    }
    public override Type StateCheck()
    {
        if (_pathListIndex >= _pathList.Count)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            return typeof(LookState);
        }
        return null;
    }
    public override void StateUpdate()
    {
        FieldOfView();
    }
    public override void StateFixedUpdate()
    {
        Vector3 moveDirection = (_currentPath - _stateObject.transform.position).normalized;

        _stateObject.transform.RotateToTarget(moveDirection, _rotationSpeed);

        _rigidbody.MoveToTarget(moveDirection, _movementSpeed);
    }
    public override void StateExit()
    {
    }
    
    
    private void FieldOfView()
    {
        if(_player == null)
        {
            return;
        }

        if(DetectionUtils.CalculateAngle(_stateObject.transform, _player, _viewAngle) && 
           DetectionUtils.CalculateDistance(_stateObject.transform, _player, _viewDistance))
        {
            DetectionUtils.RayCast(_stateObject.transform, _player, _viewDistance, _offSet, _targetLayer);
        }
    }
    public void CalculateTracePath()
    {
        var targetPosition = _stateObject.SoundPosition;

        AIUtil.CalculatePath(_pathList, _stateObject.transform.position, targetPosition, _path);

        if (_pathList.Count >= 1)
        {
            _pathListIndex = 0;

            _currentPath = _pathList[_pathListIndex];

            _rigidbody.linearVelocity = Vector3.zero;   
        }
    }
    private void NextTarget()
    {
        _pathListIndex++;

        if (_pathListIndex < _pathList.Count)
        {
            _currentPath = _pathList[_pathListIndex];
        }
        else
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _state.ChangeObjectState(InvadeState.Look);
        }
    }
}
