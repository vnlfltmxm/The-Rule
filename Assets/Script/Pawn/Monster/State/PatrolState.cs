using System;
using System.Collections.Generic;
using LN;
using Script.Manager.Framework;
using Script.Pawn;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private Transform[] _destinationArray;
    private Transform _currentDestination;

    private int _currentIndex;
    private string _patrolAreaName;
    
    
    protected NavMeshPath _path;
    protected NavMeshAgent _agent;
    protected Rigidbody _rigidbody;
    protected List<Vector3> _pathList;
    protected Vector3 _currentPath;

    protected float _rotationSpeed;
    protected float _movementSpeed;
    protected int _pathListIndex;
    
    
    public override void InitState(Pawn stateObject)
    {
        base.InitState(stateObject);
        //_patrolAreaName = _stateObject.AreaName;
    }
    public override void StateEnter()
    {
        InitializeDestination();
        SetPath();
    }
    public override Type StateCheck()
    {
        if (Vector3.Distance(_stateObject.transform.position, _currentPath) < 0.1f)
        {
            _pathListIndex++;

            if (_pathListIndex < _pathList.Count)
            {
                _currentPath = _pathList[_pathListIndex];
            }
            else
            {
                _rigidbody.linearVelocity = Vector3.zero;
                return typeof(IdleState);
            }
        }

        return null;
    }
    
    public override void StateFixedUpdate()
    {
        Vector3 moveDirection = (_currentPath - _stateObject.transform.position).normalized;

        _stateObject.transform.RotateToTarget(moveDirection, _rotationSpeed);

        _rigidbody.MoveToTarget(moveDirection, _movementSpeed);

        
    }
    public override void StateExit()
    {
        _pathListIndex = 0;
    }
    
    private void InitializeDestination()
    {
        if (_currentIndex == 0)
        {
            DestinationManager.Instance.GetRandomWayPoints(_patrolAreaName);
        }

        do
        {
            _currentDestination = _destinationArray[_currentIndex];

            _currentIndex = (_currentIndex + 1) % _destinationArray.Length;
        }
        while (Vector3.Distance(_currentDestination.position, _stateObject.transform.position) < 0.1f);
    }
    private void SetPath()
    {
        AIUtil.CalculatePath(_pathList, _stateObject.transform.position, _currentDestination.position, _path);

        if(_pathList.Count >= 1)
        {
            _currentPath = _pathList[_pathListIndex];
        }
    }
}
