using System;
using System.Collections.Generic;
using Script.Util;
using TMPro;
using UnityEngine;

public class DebugUI : UIBase
{
    [SerializeField] private TextMeshProUGUI _debugTextUI;
    protected override void OnAwake()
    {
        base.OnAwake();
    }

    public void RefreshDebugText()
    {
        string context = "";
        foreach (var debugContext in DebugManager.Instance.DebugContextList)
        {
            context += $"{debugContext.Type}: {debugContext.Context} \n";
        }

        _debugTextUI.text = context;
    }
    
    

    private void OnEnable()
    {
        DebugManager.Instance.OnDebugDataChangeEvent += RefreshDebugText;
    }
    private void OnDisable()
    {
        DebugManager.Instance.OnDebugDataChangeEvent -= RefreshDebugText;
    }

    private void OnValidate()
    {
        _debugTextUI = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
}
