using UnityEngine;
using UnityEngine.AI;

public class InvadeObjectState
{
    public InvadeObjectState(InvadeObject invadeObject)
    {
        _invadeObject = invadeObject;
        _agent = invadeObject.GetComponent<NavMeshAgent>();
        _state = invadeObject.GetComponent<InvadeStateMachine>();
    }

    protected InvadeObject _invadeObject;
    protected InvadeStateMachine _state;
    protected NavMeshAgent _agent;
}
