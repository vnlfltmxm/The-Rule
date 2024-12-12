using UnityEngine;

public class InvadeObjectPatrol : InvadeObjectMovement, IObjectState<InvadeObjectPatrol>
{
    public InvadeObjectPatrol(InvadeObject invadeObject) : base(invadeObject)
    {
        _patrolAreaName = invadeObject.AreaName;
    }

    private Transform[] _destinationArray;
    private Transform _currentDestination;

    private int _currentIndex;
    private string _patrolAreaName;
    
    public void StateEnter()
    {
        InitializeDestination();

        SetPath();
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

    private void SetPath()
    {
        CalculatePath(_pathList, _currentDestination.position, _path);

        if(_pathList.Count >= 1)
        {
            _currentTarget = _pathList[_pathListIndex];
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

    public void StateFixedUpdate()
    {
        Vector3 moveDirection = (_currentTarget - _invadeObject.transform.position).normalized;

        RotateToTarget(moveDirection, _rotationSpeed);

        MoveToTarget(moveDirection, _movementSpeed);

        if (Vector3.Distance(_invadeObject.transform.position, _currentTarget) < 0.1f)
        {
            NextTarget();
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
        _pathListIndex = 0;
    }
}
