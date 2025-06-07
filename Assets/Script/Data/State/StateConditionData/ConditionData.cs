using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public abstract class ConditionData
{
    [field:SerializeField]
    public string GlobalFieldGUID { get; private set; }
    [field:SerializeField]
    public string TargetStateGUID { get; private set; }
    [field: SerializeField] 
    public CompareType CompareType { get; private set; }

    public abstract List<CompareType> GetCompareTypes();
}

