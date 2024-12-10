using System;
using System.Collections.Generic;

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
}
