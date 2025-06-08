using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    private StateMachineWindow _stateMachineWindow;
    private ScrollView _scrollView;

    private StateElement _stateElement;
    
    private List<ProgressListView> _progressListViews = new List<ProgressListView>();
    public InspectorView(StateMachineWindow stateMachineWindow)
    {
        _stateMachineWindow = stateMachineWindow;
        InitStyle();
        GenerateScrollArea(); // 추가
    }

    private void InitStyle()
    {
        style.width = 300;
        style.overflow = Overflow.Hidden;
    }
    
    private void GenerateScrollArea()
    {
        _scrollView = new ScrollView(ScrollViewMode.Vertical);
        _scrollView.style.flexGrow = 1; // 스크롤 영역이 아래 공간을 모두 차지하게 함
        _scrollView.style.overflow = Overflow.Visible;
        Add(_scrollView);
    }

    public void LoadNodeData(StateMachineNode stateMachineNode)//StateElement stateElement)
    {
        ClearListView();
        if (stateMachineNode == null)
        {
            _stateElement = null;
            return;
        }
        _stateElement = stateMachineNode.StateElement;
        
        foreach (var conditionData in _stateElement.TargetGuidConditionDictionary)
            AddListView(conditionData.Key, conditionData.Value);
        /*ClearListView();

        if (stateMachineNode == null)
        {
            _stateElement = null;
            return;
        }
        else
            _stateElement = StateMachineUtil.StateMachineData.StateElements.FirstOrDefault(x => x.Guid == stateMachineNode.StateElement.Guid);

        Debug.Log($"OnClickNode GUID {stateMachineNode.StateElement.Guid}");

        foreach (var conditionData in _stateElement.TargetGuidConditionDictionary)
            AddListView(conditionData.Key, conditionData.Value);*/
    }
    private void AddListView(string targetGuid, List<ConditionData> conditionDatas)
    {
        Debug.LogError("AddListView");
        ProgressListView progressListView = new ProgressListView(this, targetGuid, conditionDatas);
        _progressListViews.Add(progressListView);
        Add(progressListView);
    }
    private void ClearListView()
    {
        foreach (var progressListView in _progressListViews)
            Remove(progressListView);
        _progressListViews.Clear();
    }
}