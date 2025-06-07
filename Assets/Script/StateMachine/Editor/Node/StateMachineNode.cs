using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineNode : GraphElement
{
    protected StateMachineView StateMachineView;
    public string GUID;

    public StateMachineNode(StateMachineView stateMachineView, Guid guid)
    {
        StateMachineView = stateMachineView;
        GUID = guid.ToString();

        InitStyle();
        
        InstallManipulator();
    }

    #region Style
    private void InitStyle()
    {
        // 기본 스타일 설정
        style.width = 100;
        style.height = 50;
        style.backgroundColor = new Color(0.3f, 0.3f, 0.3f);
        
        style.borderBottomWidth = 1;
        style.borderLeftWidth = 1;
        style.borderRightWidth = 1;
        style.borderTopWidth = 1;
        SetBorderColor(Color.black);
    }
    protected void SetBorderColor(Color color)
    {
        style.borderBottomColor = color;
        style.borderLeftColor = color;
        style.borderRightColor = color;
        style.borderTopColor = color;
    }
    #endregion
    
    public override void OnSelected()
    {
        base.OnSelected();
        SetBorderColor(Color.yellow);
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        SetBorderColor(Color.black);
    }
    
    #region Manipulator
    void InstallManipulator()
    {
        var drawManipulator = new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("진행선 그리기", action =>
                {
                    StateMachineView.CreateLine(this);
                },
                DropdownMenuAction.AlwaysEnabled);
        });

        var deleteManipulator = new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("Delete", action =>
                {
                    StateMachineView.DeleteNode(this);
                },
                DropdownMenuAction.AlwaysEnabled);
        });
        
        this.AddManipulator(drawManipulator); // <-- 반드시 필요!
        this.AddManipulator(deleteManipulator); // <-- 반드시 필요!
    }
    #endregion

    public static StateMachineNode Factory(StateMachineView stateMachineView, Guid guid, Type stateDataType)
    {
        if (stateDataType.IsSubclassOf(typeof(StateElementData)))
            return new StateNode(stateMachineView, guid, stateDataType);
        else if (stateDataType.IsSubclassOf(typeof(ConditionData)))
            return new ConditionNode(stateMachineView, guid, stateDataType);
        return null;
    }
}
