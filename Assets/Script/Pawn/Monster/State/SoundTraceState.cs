using System;
using System.Collections.Generic;
using LN;
using Script.Manager.Framework;
using Script.Pawn;
using UnityEngine;
using UnityEngine.AI;

public class SoundTraceState : State
{
    private ISoundTrace _soundTrace;
    private Transform _player;
    
    protected NavMeshPath _path;
    protected NavMeshAgent _agent;
    protected Rigidbody _rigidbody;
    protected List<Vector3> _pathList;
    protected Vector3 _currentPath;

    protected float _soundDistance;
    
    protected float _rotationSpeed;
    protected float _movementSpeed;
    protected int _pathListIndex;
    
    protected float _viewDistance;
    protected float _viewAngle;

    protected Vector3 _offSet = new Vector3(0f, 1f, 1f);
    
    private LayerMask _targetLayer = LayerMask.GetMask("Player", "Wall");
    
    public override void InitState(Pawn stateObject)
    {
        base.InitState(stateObject);
    }

    public override void StateEnter()
    {
        CalculateTracePath();
    }
    public override Type StateCheck()
    {
        if (_stateObject is ISoundTrace soundTrace)
            _soundTrace = soundTrace;
        else
            return typeof(IdleState);
            
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
        var targetPosition = _soundTrace.SoundPosition;

        AIUtil.CalculatePath(_pathList, _stateObject.transform.position, targetPosition, _path);

        if (_pathList.Count >= 1)
        {
            _pathListIndex = 0;

            _currentPath = _pathList[_pathListIndex];

            _rigidbody.linearVelocity = Vector3.zero;   
        }
    }

    public override void AlwaysDrawGizmos()
    {
        DrawSoundRange(_soundDistance, _stateObject.transform.position, Color.red);
    }
    
    private void DrawSoundRange(float radius, Vector3 position, Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawWireSphere(position, radius);
    }
}
