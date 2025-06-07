using System;
using System.Collections.Generic;
using Script.Pawn;
using Script.Util;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public abstract class StateElement
{
    //자신 노드의 Guid
    [field:SerializeField]
    public string Guid { get; private set; }
    //목표 노드를 향한 조건들
    [field: SerializeField]
    public SerializedDictionary<string, List<ConditionData>> TargetGuidConditionDictionary { get; private set; } =
        new SerializedDictionary<string, List<ConditionData>>();
    
    
    //현재 스테이트
    public abstract StateType StateType { get; }
    protected Pawn StateObject;
    protected List<ConditionData> Conditions = new List<ConditionData>();

    
    public StateElement(Pawn owner, StateElementData stateElementData)
    {
        StateObject = owner;
        //foreach (var conditionData in stateElementData.ConditionDatas)
        //    Conditions.Add(StateConditionFactory.CreateInstance(StateObject, conditionData));
    }
    
    // 상태에 진입 했을 때
    public virtual void Enter()
    {
        //foreach (var condition in Conditions)
        //    condition.EnterCondition();
    }
    // 상태 변환 조건 체크
    public StateType CheckConditions()
    {
        //foreach (var condition in Conditions)
        //    if (condition.CheckCondition() == true)
        //        return condition.TargetState;
        
        return StateType;
    }
    // 상태가 진행 중일 때
    public virtual void Stay()
    {
        
    }
    // 상태가 종료되었을 때
    public virtual void Exit()
    {
        
    }
}