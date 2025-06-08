using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class FloatConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new() { CompareType.Greater, CompareType.Less};
    
    [field:SerializeField]
    public float ConditionValue { get; private set; }
    
    
    private FloatField _floatField;
    public override void Init(StateMachine stateMachine)
    {
        var globalField = stateMachine.GetGlobalField(GlobalFieldGUID);
        _floatField = (FloatField)globalField;
    }
    
    public override bool CheckCondition()
    {
        //GlobalFieldGUID 로 받은 값을 비교
        switch (CompareType)
        {
            case CompareType.Greater when _floatField.Value >= ConditionValue:
            case CompareType.Less when _floatField.Value <= ConditionValue:
                return true;
            default:
                return false;
        }
    }
}