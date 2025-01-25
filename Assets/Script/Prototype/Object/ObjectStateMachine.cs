using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateMachine<TObject, TEnum, TFactory> 
    where TObject : class 
    where TEnum : Enum
    where TFactory : IStateFactory<TObject, TEnum>, new()
{
    private Dictionary<TEnum, IObjectState> _stateDictionary = new Dictionary<TEnum, IObjectState>();
    private ObjectStateFactory<TObject, TEnum, TFactory> _factory;
    private IObjectState _currentState;

    public Dictionary<TEnum, IObjectState> StateDictionary => _stateDictionary;
    public IObjectState CurrentState => _currentState;
    
    public void Initialize()
    {
        _factory = new ObjectStateFactory<TObject, TEnum, TFactory>();
    }

    public void StateUpdate()
    {
        Logger.Log($"{typeof(TObject)} CurrentState : {_currentState.GetType()}", Color.red);
        _currentState.StateUpdate();
    }

    public void FixedUpdate()
    {
        _currentState.StateFixedUpdate();
    }

    public void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }

    public void StartState(TEnum state)
    {
        if(_stateDictionary.ContainsKey(state))
        {
            _currentState = _stateDictionary[state];

            _currentState.StateEnter();
        }
    }

    public void ChangeObjectState(TEnum nextState)
    {
        if(_stateDictionary.ContainsKey(nextState))
        {
            _currentState.StateExit();

            _currentState = _stateDictionary[nextState];

            _currentState.StateEnter();
        }
    }

    public void CreateState<TClass>(TClass classType)
        where TClass : class 
    {
        var stateDictionary = _factory.CreateState<TClass, TEnum>(classType);

        if(stateDictionary != null)
        {
            foreach (KeyValuePair<TEnum, IObjectState> pair in stateDictionary)
            {
                var key = pair.Key;
                var value = pair.Value;

                _stateDictionary.Add(key, value);
            }
        }
    }

    public IObjectState GetObjectState<TClass>(TClass classType, TEnum state)
        where TClass : class
    {
        if(_stateDictionary.TryGetValue(state, out IObjectState objectState))
        {
            return objectState;
        }

        CreateCurrentState(classType, state);

        return _stateDictionary[state];
    }

    private void CreateCurrentState<TClass>(TClass classType, TEnum enumType)
        where TClass : class
    {
        var currentState = _factory.CreateState(classType, enumType);

        if(currentState != null)
        {
            _stateDictionary.Add(enumType, currentState);
        }
    }
}
