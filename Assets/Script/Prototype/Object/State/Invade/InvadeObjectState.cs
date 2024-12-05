using UnityEngine;
using UnityEngine.AI;

public class InvadeObjectState
{
    public InvadeObjectState(InvadeObject invadeObject)
    {
        _invadeObject = invadeObject;
        _agent = invadeObject.GetComponent<NavMeshAgent>();
    }

    protected InvadeObject _invadeObject;
    protected NavMeshAgent _agent;
}
