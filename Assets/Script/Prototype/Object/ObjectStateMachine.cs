using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateMachine<T> where T : Enum
{
    private Dictionary<T, IObjectState> _stateDictionary = new Dictionary<T, IObjectState>();
    private ObjectStateFactory _factory;
    private IObjectState _currentState;

    public Dictionary<T, IObjectState> StateDictionary => _stateDictionary;
    public IObjectState CurrentState => _currentState;
    
    public void Initialize()
    {
        _factory = new ObjectStateFactory();
    }

    public void StateUpdate()
    {
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

    public void StartState(T state)
    {
        if(_stateDictionary.ContainsKey(state))
        {
            _currentState = _stateDictionary[state];

            _currentState.StateEnter();
        }
    }

    public void ChangeObjectState(T nextState)
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
        var stateDictionary = _factory.CreateState<TClass, T>(classType);

        if(stateDictionary != null)
        {
            foreach (KeyValuePair<T, IObjectState> pair in stateDictionary)
            {
                var key = pair.Key;
                var value = pair.Value;

                _stateDictionary.Add(key, value);
            }
        }
    }

    public IObjectState GetObjectState<TClass,TEnum>(TClass classType, T state)
        where TClass : class
        where TEnum : Enum
    {
        if(_stateDictionary.TryGetValue(state, out IObjectState objectState))
        {
            return objectState;
        }

        CreateCurrentState(classType, state);

        return _stateDictionary[state];
    }

    private void CreateCurrentState<TClass>(TClass classType, T enumType)
        where TClass : class
    {
        var currentState = _factory.CreateState(classType, enumType);

        if(currentState != null)
        {
            _stateDictionary.Add(enumType, currentState);
        }
    }
}
