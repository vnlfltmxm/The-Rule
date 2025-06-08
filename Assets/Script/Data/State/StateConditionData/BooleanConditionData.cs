using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class BooleanConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new List<CompareType>() { CompareType.True, CompareType.False };

    private BooleanField _booleanField;
    public override void Init(StateMachine stateMachine)
    {
        var globalField = stateMachine.GetGlobalField(GlobalFieldGUID);
        _booleanField = (BooleanField)globalField;
    }
    
    public override bool CheckCondition()
    {
        //GlobalFieldGUID 로 받은 값을 비교
        switch (CompareType)
        {
            case CompareType.True when _booleanField.Value == true:
            case CompareType.False when _booleanField.Value == false:
                return true;
            default:
                return false;
        }
    }
}