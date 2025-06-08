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
    public string Guid { get; set; }
    /// <summary>
    /// 목표 노드를 향한 조건들
    /// Key=목표Guid
    /// Value=조건들
    /// </summary>
    [field: SerializeField]
    public SerializedDictionary<string, List<ConditionData>> TargetGuidConditionDictionary { get; private set; } =
        new SerializedDictionary<string, List<ConditionData>>();
    
    protected StateMachine StateMachine;
    
    [field: SerializeField]
    public Vector2 NodePosition { get; set; }
    
    // 상태에 진입 했을 때
    public virtual void Enter()
    {
        //foreach (var condition in Conditions)
        //    condition.EnterCondition();
    }
    // 상태 변환 조건 체크
    public string CheckConditions()
    {
        foreach (var targetGuidCondition in TargetGuidConditionDictionary)
        {
            bool isSuitable = true;
            foreach (ConditionData conditionData in targetGuidCondition.Value)
            {
                //하나라도 틀리면 
                if (conditionData.CheckCondition() == false)
                {
                    isSuitable = false;
                    break;
                }
            }
            if(isSuitable == true)
                return targetGuidCondition.Key;
        }
        return null;
    }
    // 상태가 진행 중일 때
    public virtual void Stay()
    {
        
    }
    // 상태가 종료되었을 때
    public virtual void Exit()
    {
        
    }

    public void NewConditionDictionary(string targetGuid)
    {
        if (TargetGuidConditionDictionary.TryGetValue(targetGuid, out List<ConditionData> dataList))
        {
            if (dataList == null)
                dataList = new List<ConditionData>();
            else
                dataList.Clear();
        }
        else
        {
            List<ConditionData> datas = new List<ConditionData>();
            TargetGuidConditionDictionary.Add(targetGuid, datas);
        }
    }
    public void AddConditionData(string targetGuid, ConditionData conditionData)
    {
        if (TargetGuidConditionDictionary.TryGetValue(targetGuid, out List<ConditionData> dataList))
        {
            if (dataList == null)
                dataList = new List<ConditionData>();
            if(!dataList.Contains(conditionData))
                dataList.Add(conditionData);
        }
        else
        {
            List<ConditionData> datas = new List<ConditionData>();
            datas.Add(conditionData);
            TargetGuidConditionDictionary.Add(targetGuid, datas);
        }
    }
    public void ChangeConditionData(string targetGuid, ConditionData prevConditionData, ConditionData newConditionData)
    {
        if (TargetGuidConditionDictionary.TryGetValue(targetGuid, out List<ConditionData> dataList))
        {
            if (dataList == null)
                dataList = new List<ConditionData>();
            
            if(dataList.Contains(prevConditionData))
                dataList.Remove(prevConditionData);
            if(!dataList.Contains(newConditionData))
                dataList.Add(newConditionData);
        }
        else
        {
            List<ConditionData> datas = new List<ConditionData>();
            datas.Add(newConditionData);
            TargetGuidConditionDictionary.Add(targetGuid, datas);
        }
    }
}