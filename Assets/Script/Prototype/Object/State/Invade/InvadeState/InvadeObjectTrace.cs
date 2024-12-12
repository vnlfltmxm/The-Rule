using UnityEngine;

public class InvadeObjectTrace : InvadeObjectMovement, IObjectState<InvadeObjectTrace>, ISetPlayer
{
    public InvadeObjectTrace(InvadeObject invadeObject) : base(invadeObject) { }
    
    private Transform _player;

    #region StateMethods
    public void StateEnter()
    {
        CalculateTracePath();
    }

    public void StateFixedUpdate()
    {
        Vector3 moveDirection = (_currentTarget - _invadeObject.transform.position).normalized;

        RotateToTarget(moveDirection, _rotationSpeed);

        MoveToTarget(moveDirection, _movementSpeed);

        if(Vector3.Distance(_currentTarget, _invadeObject.transform.position) < 0.1f)
        {
            NextTarget();
        }
    }

    public void StateUpdate()
    {
        FieldOfView();
    }

    #endregion

    private void FieldOfView()
    {
        if(_player == null)
        {
            return;
        }

        if(CalculateAngle(_player, _invadeObject.transform)
            && CalculateDistance(_player, _invadeObject.transform))
        {
            RayCast(_player, _invadeObject.transform, InvadeState.Tracking);
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

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
}
