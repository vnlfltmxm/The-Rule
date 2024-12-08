using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvadeObjectPatrol : InvadeObjectState, IObjectState<InvadeObjectPatrol>
{
    public InvadeObjectPatrol(InvadeObject invadeObject) : base(invadeObject)
    {
        _patrolAreaName = invadeObject.AreaName;
        _rotationSpeed = invadeObject.RotationSpeed;
        _moveSpeed = invadeObject.ObjectSpeed;

        _path = new NavMeshPath();  
    }

    private Transform[] _destinationArray;
    private Transform _currentDestination;
    private NavMeshPath _path;

    private List<Vector3> _pathList = new List<Vector3>();
    private Vector3 _currentTarget;

    private int _currentIndex;
    private int _currentPathIndex;
    private string _patrolAreaName;

    private float _rotationSpeed;
    private float _moveSpeed;

    public void StateEnter()
    {
        InitializeDestination();

        CalculatePath();
    }

    private void InitializeDestination()
    {
        if (_currentIndex == 0)
        {
            RandomTransformArray();
        }

        do
        {
            _currentDestination = _destinationArray[_currentIndex];

            _currentIndex = (_currentIndex + 1) % _destinationArray.Length;
        }
        while (Vector3.Distance(_currentDestination.position, _invadeObject.transform.position) < 0.1f);
    }

    private void CalculatePath()
    {
        _pathList.Clear();

        var sourcePosition = _invadeObject.transform.position;

        var targetPosition = _currentDestination.position;

        NavMesh.CalculatePath(sourcePosition, targetPosition, NavMesh.AllAreas, _path);

        for (int i = 1; i < _path.corners.Length; i++)
        {
            _pathList.Add(_path.corners[i]);
        }

        if (_pathList.Count >= 1)
        {
            _currentTarget = _pathList[_currentPathIndex];
        }
    }

    private void RandomTransformArray()
    {
        _destinationArray = DestinationManager.Instance.GetWayPoints(_patrolAreaName);

        if(_destinationArray != null)
        {
            for (int i = _destinationArray.Length - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);

                var temp = _destinationArray[i];

                _destinationArray[i] = _destinationArray[randomIndex];

                _destinationArray[randomIndex] = temp;
            }
        }
    }

    public void StateUpdate()
    {
        Vector3 moveDirection = (_currentTarget - _invadeObject.transform.position).normalized;

        RotateToTarget(moveDirection);

        MoveToTarget(moveDirection);

        if(Vector3.Distance(_invadeObject.transform.position, _currentTarget) < 0.1f)
        {
            NextTarget();
        }
    }

    private void RotateToTarget(Vector3 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

        _invadeObject.transform.rotation = Quaternion.Slerp(_invadeObject.transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void MoveToTarget(Vector3 moveDirection)
    {
        Vector3 moveVelocity = moveDirection * _moveSpeed;

        moveVelocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = moveVelocity;
    }

    private void NextTarget()
    {
        _currentPathIndex++;

        if (_currentPathIndex < _pathList.Count)
        {
            _currentTarget = _pathList[_currentPathIndex];
        }
        else
        {
            EndPath();
        }
    }

    private void EndPath()
    {
        _rigidbody.linearVelocity = Vector3.zero;

        _state.ChangeObjectState(InvadeState.Idle);
    }

    public void StateExit()
    {
        _currentPathIndex = 0;
    }
}
