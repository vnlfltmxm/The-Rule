using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;
using UnityEngine.AI;

public class MomStateMachine : MonoBehaviour
{
    private ObjectStateMachine<MomMonster, MomState, MomMonsterStateFactory> _stateMachine;
    private MomMonster _stateObject;
    private NavMeshAgent _agent;
    
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
        
        _agent = _stateObject.GetComponent<NavMeshAgent>();
    }
    
    private void Start()
    {
        SetAllowedAreas(_stateObject.AreaType);
        _stateMachine.StartState(MomState.Tracking);
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
    
    private void SetAllowedAreas(AreaType allowedAreas)
    {
        int mask = 0;
        foreach (AreaType area in Enum.GetValues(typeof(AreaType)))
        {
            if (area == AreaType.Null) continue; // Null은 제외

            if (allowedAreas.HasFlag(area))
            {
                int areaIndex = NavMesh.GetAreaFromName(area.ToString());
                if (areaIndex != -1)
                {
                    mask |= 1 << areaIndex;
                }
            }
        }
        _agent.areaMask = mask;
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
