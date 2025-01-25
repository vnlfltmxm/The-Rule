using System.Collections.Generic;
using UnityEngine;

public class ChildStateMachine : MonoBehaviour
{
    private ObjectStateMachine<ChildMonster, ChildState, ChildMonsterStateFactory> _stateMachine;
    private ChildMonster _stateObject;
    
    private void Awake()
    {
        InitializeOnAwake();
    }

    private void InitializeOnAwake()
    {
        _stateObject = GetComponent<ChildMonster>();

        _stateMachine = new ObjectStateMachine<ChildMonster, ChildState, ChildMonsterStateFactory>();

        _stateMachine.Initialize();

        _stateMachine.CreateState(_stateObject);
    }
    
    private void Start()
    {
        _stateMachine.StartState(ChildState.Idle);
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

    public void ChangeObjectState(ChildState newState)
    {
        _stateMachine.ChangeObjectState(newState);
    }

    public IObjectState GetCurrentObjectState()
    {
        return _stateMachine.CurrentState;
    }

    public Dictionary<ChildState, IObjectState> GetStateDictionary()
    {
        return _stateMachine.StateDictionary;
    }

    public T GetObjectState<T>(ChildState state) where T : class
    {
        var objectState = _stateMachine.GetObjectState<ChildMonster>(_stateObject, state);

        if(objectState is T tValue)
        {
            return tValue;
        }
        else
        {
            Logger.Log("T ��ȯ ����");
            return null;
        }
    }
}
