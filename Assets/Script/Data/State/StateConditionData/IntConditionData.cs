using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class IntConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new() { CompareType.Greater, CompareType.Less, CompareType.Equals, CompareType.NotEqual };
    
    [field:SerializeField]
    public int ConditionValue { get; private set; }
    
    
    private IntField _intField;
    public override void Init(StateMachine stateMachine)
    {
        var globalField = stateMachine.GetGlobalField(GlobalFieldGUID);
        _intField = (IntField)globalField;
    }
    
    public override bool CheckCondition()
    {
        //GlobalFieldGUID 로 받은 값을 비교
        //int globalFieldValue = 1;
        switch (CompareType)
        {
            case CompareType.Greater when _intField.Value >= ConditionValue:
            case CompareType.Less when _intField.Value <= ConditionValue:
            case CompareType.Equals when _intField.Value == ConditionValue:
            case CompareType.NotEqual when _intField.Value != ConditionValue:
                return true;
            default:
                return false;
        }
    }
}