using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class DebugManager : SingletonMonoBehaviour<DebugManager>
{
    private List<DebugContext> _debugContextList = new List<DebugContext>();
    public IReadOnlyList<DebugContext> DebugContextList => _debugContextList;

    public event Action OnDebugDataChangeEvent;

    protected override void OnAwake()
    {
        foreach (DebugType type in Enum.GetValues(typeof(DebugType)))
        {
            _debugContextList.Add(new DebugContext() { Type = type, Context = "" });
        }
    }

    public void SetDebugData(DebugType type, string context)
    {
        _debugContextList[(int)type].Context = context;
        OnDebugDataChangeEvent?.Invoke();
    }
}

public class DebugContext
{
    public DebugType Type;
    public string Context;
}
