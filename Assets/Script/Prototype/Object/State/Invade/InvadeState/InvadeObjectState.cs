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

        _agent.enabled = false;
    }

    protected InvadeObject _invadeObject;
    protected InvadeStateMachine _state;
    protected NavMeshAgent _agent;
    protected Rigidbody _rigidbody;
}
