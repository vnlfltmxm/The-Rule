using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class UnityObjectConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new List<CompareType>() { CompareType.IsNull, CompareType.IsNotNull, CompareType.Equals , CompareType.NotEqual};
    
    [field:SerializeField]
    public Object ConditionValue { get; private set; }


    private GameObjectField _gameObjectField;
    public override void Init(StateMachine stateMachine)
    {
        var globalField = stateMachine.GetGlobalField(GlobalFieldGUID);
        _gameObjectField = (GameObjectField)globalField;
    }
    
    public override bool CheckCondition()
    {
        //GlobalFieldGUID 로 받은 값을 비교
        switch (CompareType)
        {
            case CompareType.IsNull when ReferenceEquals(_gameObjectField.Value, null):
            case CompareType.IsNotNull when !ReferenceEquals(_gameObjectField.Value, null):
            case CompareType.Equals when ReferenceEquals(_gameObjectField.Value, ConditionValue):
            case CompareType.NotEqual when !ReferenceEquals(_gameObjectField.Value, ConditionValue):
                return true;
            default:
                return false;
        }
    }
}