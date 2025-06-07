using System.Collections.Generic;
using Script.Util;

public class IntConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new() { CompareType.Greater, CompareType.Less, CompareType.Equals, CompareType.NotEqual };
}