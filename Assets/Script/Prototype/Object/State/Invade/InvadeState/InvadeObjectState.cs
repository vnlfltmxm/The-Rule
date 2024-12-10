using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvadeObjectState
{
    public InvadeObjectState(InvadeObject invadeObject)
    {
        _invadeObject = invadeObject;

        _state = invadeObject.GetComponent<InvadeStateMachine>();
        _agent = invadeObject.GetComponent<NavMeshAgent>();
        _rigidbody = invadeObject.GetComponent<Rigidbody>();

        _path = new NavMeshPath();
        _agent.enabled = false;
    }

    protected InvadeObject _invadeObject;
    protected InvadeStateMachine _state;
    protected NavMeshAgent _agent;
    protected NavMeshPath _path;
    protected Rigidbody _rigidbody;

    protected void CalculatePath(List<Vector3> pathList, Vector3 targetPosition, NavMeshPath path)
    {
        pathList.Clear();

        var sourcePosition = _invadeObject.transform.position;

        NavMesh.CalculatePath(sourcePosition, targetPosition, NavMesh.AllAreas, path);

        for(int i = 1; i < path.corners.Length; i++)
        {
            pathList.Add(path.corners[i]);
        }
    }

    protected void RotateToTarget(Vector3 moveDirection, float rotationSpeed)
    {
        float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

        _invadeObject.transform.rotation = Quaternion.Slerp(_invadeObject.transform.rotation,
            rotation, rotationSpeed * Time.fixedDeltaTime);
    }

    protected void MoveToTarget(Vector3 moveDirection, float moveSpeed)
    {
        Vector3 moveVelocity = moveDirection * moveSpeed;

        moveVelocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = moveVelocity;
    }
}
