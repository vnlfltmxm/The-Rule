using System.Collections.Generic;
using UnityEngine.UIElements;

public class ProgressListView : VisualElement
{
    private InspectorView _inspectorView;

    private string _targetGuid;
    private List<ConditionData> _conditionDataList;
    private List<CompareElement> _compareElements = new List<CompareElement>();
    
    public ProgressListView(InspectorView inspectorView, string targetGuid, List<ConditionData> conditionDataList)
    {
        _inspectorView = inspectorView;
        _targetGuid = targetGuid;
        _conditionDataList = conditionDataList;
        InitStyle();

        LoadConditionData();
    }

    private void InitStyle()
    {
        
    }

    private void LoadConditionData()
    {
        ClearElements();
        foreach (var conditionData in _conditionDataList)
            AddElement(conditionData);
    }

    #region ElementControl
    private void AddElement(ConditionData conditionData)
    {
        CompareElement compareElement = new CompareElement(this, conditionData);
        _compareElements.Add(compareElement);
        Add(compareElement);
    }
    private void ClearElements()
    {
        foreach (var relationElement in _compareElements)
            Remove(relationElement);
        _compareElements.Clear();
    }
    #endregion
    
    #region DataControl

    private void AddTempData()
    {
        ConditionData conditionData = new IntConditionData();
        _conditionDataList.Add(conditionData);
        AddElement(conditionData);
    }
    private void RemoveData(ConditionData conditionData)
    {
        CompareElement removeCompareElement = null;
        foreach (var element in _compareElements)
        {
            if (element.ConditionData == conditionData)
            {
                removeCompareElement = element;
                _conditionDataList.Remove(conditionData);
            }
        }

        if (removeCompareElement != null)
        {
            _compareElements.Remove(removeCompareElement);
            Remove(removeCompareElement);
        }
    }
    private void RemoveData(CompareElement compareElement)
    {
        foreach (var element in _compareElements)
        {
            if (element == compareElement)
            {
                _conditionDataList.Remove(element.ConditionData);
                Remove(element);
            }
        }
    }
    #endregion
}