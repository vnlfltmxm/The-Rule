using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class InvadeObjectMovement : InvadeObjectState
{
    public InvadeObjectMovement(InvadeObject invadeObject) : base(invadeObject)
    {
        _pathList = new List<Vector3>();
        _path = new NavMeshPath();

        _agent = invadeObject.GetComponent<NavMeshAgent>();
        _rigidbody = invadeObject.GetComponent<Rigidbody>();

        _rotationSpeed = _data.RotationSpeed;
        _movementSpeed = _data.Speed;
    }

    protected NavMeshPath _path;
    protected NavMeshAgent _agent;
    protected Rigidbody _rigidbody;
    protected List<Vector3> _pathList;
    protected Vector3 _currentTarget;

    protected float _rotationSpeed;
    protected float _movementSpeed;
    protected int _pathListIndex;

    protected void CalculatePath(List<Vector3> pathList, Vector3 targetPosition, NavMeshPath path)
    {
        pathList.Clear();

        var sourcePosition = _invadeObject.transform.position;

        NavMesh.CalculatePath(sourcePosition, targetPosition, NavMesh.AllAreas, path);

        for (int i = 1; i < path.corners.Length; i++)
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
