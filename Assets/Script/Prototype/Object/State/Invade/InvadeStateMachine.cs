using UnityEngine;

public class InvadeStateMachine : MonoBehaviour
{
    private ObjectStateMachine<InvadeState> _stateMachine;
    private InvadeObject _invadeObject;

    private void Awake()
    {
        InitializeOnAwake();
    }

    private void InitializeOnAwake()
    {
        _invadeObject = GetComponent<InvadeObject>();

        _stateMachine = new ObjectStateMachine<InvadeState>();

        _stateMachine.Initialize();

        _stateMachine.CreateState(_invadeObject);
    }

    private void Start()
    {
        _stateMachine.StartState(InvadeState.Patrol);
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeObjectState(InvadeState newState)
    {
        _stateMachine.ChangeObjectState(newState);
    }

    public IObjectState GetObjectState()
    {
        return _stateMachine.CurrentState;
    }

}
