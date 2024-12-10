using System.Collections.Generic;
using UnityEngine;

public class InvadeObjectTrace : InvadeObjectState, IObjectState<InvadeObjectTrace>
{
    public InvadeObjectTrace(InvadeObject invadeObject) : base(invadeObject)
    {
        _pathList = new List<Vector3>();

        _rotationSpeed = invadeObject.RotationSpeed;
        _moveSpeed = invadeObject.ObjectSpeed;
    }

    private List<Vector3> _pathList;
    private Vector3 _currentTarget;

    private float _rotationSpeed;
    private float _moveSpeed;
    private int _pathListIndex;

    public void StateEnter()
    {
        CalculateTracePath();
    }

    public void StateFixedUpdate()
    {
        Vector3 moveDirection = (_currentTarget - _invadeObject.transform.position).normalized;

        RotateToTarget(moveDirection, _rotationSpeed);

        MoveToTarget(moveDirection, _moveSpeed);

        if(Vector3.Distance(_currentTarget, _invadeObject.transform.position) < 0.1f)
        {
            NextTarget();
        }
    }

    public void CalculateTracePath()
    {
        var targetPosition = _invadeObject.SoundPosition;

        CalculatePath(_pathList, targetPosition, _path);

        if (_pathList.Count >= 1)
        {
            _pathListIndex = 0;
            _currentTarget = _pathList[_pathListIndex];

            _rigidbody.linearVelocity = Vector3.zero;   
        }
    }

    private void NextTarget()
    {
        _pathListIndex++;

        if (_pathListIndex < _pathList.Count)
        {
            _currentTarget = _pathList[_pathListIndex];
        }
        else
        {
            _rigidbody.linearVelocity = Vector3.zero;

            _state.ChangeObjectState(InvadeState.Look);
        }
    }
}
