using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    private StateMachineWindow _stateMachineWindow;
    private ScrollView _scrollView;

    private StateElementData _stateElementData;
    
    private List<ProgressListView> _progressListViews = new List<ProgressListView>();
    public InspectorView(StateMachineWindow stateMachineWindow)
    {
        _stateMachineWindow = stateMachineWindow;
        InitStyle();
        GenerateScrollArea(); // 추가
    }

    private void InitStyle()
    {
        
    }
    
    private void GenerateScrollArea()
    {
        _scrollView = new ScrollView(ScrollViewMode.Vertical);
        _scrollView.style.flexGrow = 1; // 스크롤 영역이 아래 공간을 모두 차지하게 함
        _scrollView.style.overflow = Overflow.Visible;
        Add(_scrollView);
    }

    public void LoadNodeData(StateElementData stateElementData)
    {
        _stateElementData = stateElementData;
        ClearListView();
        
        foreach (var conditionData in _stateElementData.TargetGuidConditionDictionary)
            AddListView(conditionData.Key, conditionData.Value);
    }
    private void AddListView(string targetGuid, List<ConditionData> conditionDatas)
    {
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