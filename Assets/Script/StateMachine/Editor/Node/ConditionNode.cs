using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ConditionNode : StateMachineNode
{
    private StateMachineView _stateMachineView;
    public string GUID;
    public Type ConditionType;
    
    public ConditionNode(StateMachineView stateMachineView, Guid guid, Type conditionType) : base(stateMachineView, guid)
    {
        ConditionType = conditionType;
        
        var conditionLabel = new Label(ConditionType.ToString());
        conditionLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        conditionLabel.style.flexGrow = 1;
        conditionLabel.pickingMode = PickingMode.Ignore;
        Add(conditionLabel);
        
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
