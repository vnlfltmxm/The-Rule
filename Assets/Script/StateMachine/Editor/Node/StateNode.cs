using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateNode : StateMachineNode
{
    public Type StateType;
    public bool EntryPoint = false;
    
    public StateNode(StateMachineView stateMachineView, Guid guid, Type stateType) : base(stateMachineView, guid)
    {
        StateType = stateType;

        var stateLabel = new Label(StateType.ToString());
        stateLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        stateLabel.style.flexGrow = 1;
        stateLabel.pickingMode = PickingMode.Ignore;
        Add(stateLabel);
        
        // 클릭 및 드래그 기능 활성화
        this.capabilities |= Capabilities.Selectable | Capabilities.Movable;

        // 선택 및 드래그 가능하도록 조작기(Manipulator) 추가
        this.AddManipulator(new Dragger());
    }

    //요소 추가후 해당함수 호출시 적용
    public void RefreshExpandedState()
    {
        
    }
}
