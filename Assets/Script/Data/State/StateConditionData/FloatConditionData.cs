using System.Collections.Generic;
using Script.Util;

public class FloatConditionData : ConditionData
{
    public override List<CompareType> GetCompareTypes() => new() { CompareType.Greater, CompareType.Less};
}