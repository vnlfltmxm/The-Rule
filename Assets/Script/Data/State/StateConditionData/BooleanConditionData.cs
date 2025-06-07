using System.Collections.Generic;
using Script.Util;

public class BooleanConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new List<CompareType>() { CompareType.True, CompareType.False };
}