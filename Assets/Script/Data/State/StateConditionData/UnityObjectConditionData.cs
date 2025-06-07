using System.Collections.Generic;
using Script.Util;

public class UnityObjectConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new List<CompareType>() { CompareType.IsNull, CompareType.IsNotNull, CompareType.Equals , CompareType.NotEqual};
}