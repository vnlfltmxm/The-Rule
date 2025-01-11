using System;
using System.Collections.Generic;
using LN;
using Script.Pawn;
using UnityEngine;

public partial class MonsterBase
{
    private Dictionary<Type, State> _stateDictionary = new Dictionary<Type, State>();
    private State _currentState;
    
    protected State GetState(Type type) => _stateDictionary[type];
    protected void ChangeState(Type type)
    {
        if (type == null)
            return;
        _currentState?.StateExit();
        _currentState = GetState(type);
        _currentState.StateEnter();
    }

    protected void AddState<TState>(Pawn pawn) where TState : State, new() 
    {
        _stateDictionary.Add(typeof(TState), StateFactory.CreateState<TState>(pawn));
    }

    protected virtual void InitFSM()
    {
        
    }
    
    protected void UpdateFSM()
    {
        _currentState?.StateUpdate();

        // 상태 전환 조건 확인
        Type nextStateType = _currentState?.StateCheck();
        ChangeState(nextStateType);
    }
    protected void FixedUpdateFSM()
    {
        _currentState?.StateFixedUpdate();
    }
    protected void LateUpdateFSM()
    {
        _currentState?.StateLateUpdate();
    }
    /*public void ChangeState<TState>() where TState : State
    {
        _currentState?.Exit(); // 기존 상태 종료
        _currentState = newState;
        _currentState.Enter(); // 새로운 상태 진입
    }*/
}
