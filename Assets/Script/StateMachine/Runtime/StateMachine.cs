using System;
using System.Collections.Generic;
using System.Linq;
using Script.Pawn;
using Script.Util;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachine : MonoBehaviour
{
    //[SerializeField] private List<GlobalField> _globalFieldDatas;
    //[SerializeField, HideInInspector] private List<StateElementData> _stateElementDatas;
    [SerializeField] private StateMachineData _stateMachineData;
    public StateMachineData StateMachineData { get => _stateMachineData; set => _stateMachineData = value; }

    //private List<StateElement> _stateElements = new List<StateElement>();
    private StateElement _currentState;
    
    private Pawn _stateObject;

    private void Awake()
    {
        InitializeOnAwake();
    }

    private void InitializeOnAwake()
    {
        _stateObject = GetComponent<Pawn>();

        /*foreach (var stateElementData in _stateData.StateElementDatas)
            _stateElements.Add(StateElementFactory.CreateInstance(_stateObject, stateElementData));*/
    }
    
    private void Start()
    {
        if (_stateMachineData.StateElements.Count == 0)
        {
            Logger.LogError($"StateElement가 초기화되지 않았습니다");
            return;
        }
        _currentState = _stateMachineData.StateElements.First();
        _currentState.Enter();
    }

    private void Update()
    {
        if (_stateMachineData.StateElements.Count == 0)
            return;
        
        //상태 체크후 가져온 타입의 상태 가져옴
        StateElement nextState = CheckConditions();
        
        _currentState.Stay();
        if (nextState.Equals(_currentState) == false)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }
    }

    private StateElement CheckConditions()
    {
        //_currentState.Guid;
        //_currentState.CheckConditions();

        string targetGuid = _currentState.CheckConditions();
        return GetStateElement(targetGuid);
    }

    public StateElement GetStateElement(string Guid)
    {
        if (Guid == null)
            return null;
        return _stateMachineData.StateElements.FirstOrDefault(stateElement => stateElement.Guid == Guid);
    }
    public GlobalField GetGlobalField(string Guid)
    {
        if (Guid == null)
            return null;

        //_stateMachineData.GlobalFields.FirstOrDefault()
        return _stateMachineData.GlobalFields.Find(x => x.Guid == Guid);
        //return _stateMachineData.GlobalFields.TryGetValue(Guid, out GlobalField globalField) ? globalField : null;
        //return _stateMachineData.GlobalFieldDictionary.TryGetValue(Guid, out GlobalField globalField) ? globalField : null;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        _stateMachine.OnTriggerEnter(other);
    }

    public void ChangeObjectState(ChildState newState)
    {
        _stateMachine.ChangeObjectState(newState);
    }*/
}
