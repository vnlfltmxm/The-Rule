using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvadeObjectPatrol : InvadeObjectState, IObjectState<InvadeObjectPatrol>
{
    public InvadeObjectPatrol(InvadeObject invadeObject) : base(invadeObject)
    {
        _patrolAreaName = invadeObject.AreaName;

        _agentPath = new NavMeshPath();
    }

    private Transform[] _destinationArray;
    private Transform _currentDestination;
    private NavMeshPath _agentPath;

    private List<Vector3> _pathList = new List<Vector3>();

    private int _currentIndex;
    private int _pathListIndex;
    private float _offSet = 1f;
    private string _patrolAreaName;

    public void StateEnter()
    {
        if(_destinationArray == null || _currentIndex > _destinationArray.Length - 1)
        {
            _currentIndex = 0;

            _destinationArray = DestinationManager.Instance.GetWayPoints(_patrolAreaName);
        }

        _currentDestination = _destinationArray[_currentIndex];

        _currentIndex++;

        _agent.CalculatePath(_currentDestination.position, _agentPath);

        if(_agentPath.status == NavMeshPathStatus.PathComplete)
        {
            Vector3[] corners = _agentPath.corners;

            bool cornserLength = corners.Length > 2;

            if (cornserLength)
            {
                for (int i = 1; i < corners.Length - 1; i++)
                {
                    Vector3 currentCorner = corners[i];

                    Vector3 directionToPreviousCorner = (corners[i - 1] - corners[i]).normalized;

                    Vector3 directionToNextConrner = (corners[i + 1] - corners[i]).normalized;

                    Vector3 normal = -(directionToNextConrner + directionToPreviousCorner).normalized;

                    if (normal != Vector3.zero)
                    {
                        Vector3 newCorner = currentCorner + normal * _offSet;

                        corners[i] = newCorner;
                    }
                }
            }

            foreach (Vector3 corner in corners)
            {
                _pathList.Add(corner);
            }

            //Debug
            _invadeObject.PathList = _pathList;
        }

        _agent.SetDestination(_pathList[_pathListIndex]);
    }

    public void StateUpdate()
    {
        if(_agent.remainingDistance <= 0.1f)
        {
            NextPath();
        }
    }

    private void NextPath()
    {
        _pathListIndex++;

        if (_pathListIndex < _pathList.Count)
        {
            _agent.SetDestination(_pathList[_pathListIndex]);
        }
        else
        {
            _agent.SetDestination(_invadeObject.transform.position);

            _state.ChangeObjectState(InvadeState.Look);
        }
    }

    public void StateExit()
    {
        _pathList.Clear();

        _pathListIndex = 0;
    }
}
