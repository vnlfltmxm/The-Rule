using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;

[Serializable]
public abstract class ConditionData
{
    [field:SerializeField]
    public string GlobalFieldGUID { get; private set; }
    [field:SerializeField]
    public string TargetStateGUID { get; private set; }
    [field: SerializeField] 
    public CompareType CompareType { get; private set; }

    public abstract List<CompareType> GetCompareTypes();

    
    public abstract void Init(StateMachine stateMachine);
    public abstract bool CheckCondition();
}

