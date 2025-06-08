using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineNode : GraphElement
{
    protected StateMachineView StateMachineView;
    public StateElement StateElement;
    
    public bool EntryPoint = false;

    public StateMachineNode(StateMachineView stateMachineView, StateElement stateElement)
    {
        StateMachineView = stateMachineView;
        StateElement = stateElement;

        InitStyle();
        
        InstallManipulator();
        
        CreateNameLabel();
        
        // 클릭 및 드래그 기능 활성화
        this.capabilities |= Capabilities.Selectable | Capabilities.Movable;

        // 선택 및 드래그 가능하도록 조작기(Manipulator) 추가
        this.AddManipulator(new Dragger());
        
        RegisterCallback<MouseUpEvent>(OnDragEnd);
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

    private void CreateNameLabel()
    {
        var stateLabel = new Label(StateElement.GetType().ToString());
        stateLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        stateLabel.style.flexGrow = 1;
        stateLabel.pickingMode = PickingMode.Ignore;
        Add(stateLabel);
    }
    
    public override void OnSelected()
    {
        base.OnSelected();
        SetBorderColor(Color.yellow);
        StateMachineView.OnClickNode(this);
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        SetBorderColor(Color.black);
        StateMachineView.OnClickNode(null);
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
    
    private void OnDragEnd(MouseUpEvent evt)
    {
        var position = GetPosition().position; // 실제 위치 얻기
        StateElement.NodePosition = position;  // 저장
    }
}
