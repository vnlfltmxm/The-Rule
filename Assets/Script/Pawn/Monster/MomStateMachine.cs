using System.Collections.Generic;
using UnityEngine;

public class MomStateMachine : MonoBehaviour
{
    private ObjectStateMachine<MomMonster, MomState, MomMonsterStateFactory> _stateMachine;
    private MomMonster _stateObject;
    
    private void Awake()
    {
        InitializeOnAwake();
    }

    private void InitializeOnAwake()
    {
        _stateObject = GetComponent<MomMonster>();

        _stateMachine = new ObjectStateMachine<MomMonster, MomState, MomMonsterStateFactory>();

        _stateMachine.Initialize();

        _stateMachine.CreateState(_stateObject);
    }
    
    private void Start()
    {
        _stateMachine.StartState(MomState.Idle);
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

    public void ChangeObjectState(MomState newState)
    {
        _stateMachine.ChangeObjectState(newState);
    }

    public IObjectState GetCurrentObjectState()
    {
        return _stateMachine.CurrentState;
    }

    public Dictionary<MomState, IObjectState> GetStateDictionary()
    {
        return _stateMachine.StateDictionary;
    }

    public T GetObjectState<T>(MomState state) where T : class
    {
        var objectState = _stateMachine.GetObjectState<MomMonster>(_stateObject, state);

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
