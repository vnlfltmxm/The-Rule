using System.Collections.Generic;
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

    private void OnTriggerEnter(Collider other)
    {
        _stateMachine.OnTriggerEnter(other);
    }

    public void ChangeObjectState(InvadeState newState)
    {
        _stateMachine.ChangeObjectState(newState);
    }

    public IObjectState GetCurrentObjectState()
    {
        return _stateMachine.CurrentState;
    }

    public Dictionary<InvadeState, IObjectState> GetStateDictionary()
    {
        return _stateMachine.StateDictionary;
    }

    public T GetObjectState<T>(InvadeState state) where T : class
    {
        var objectState = _stateMachine.GetObjectState<InvadeObject, InvadeState>(_invadeObject, state);

        if(objectState is T tValue)
        {
            return tValue;
        }
        else
        {
            Logger.Log("T 변환 실패");
            return null;
        }
    }

}
