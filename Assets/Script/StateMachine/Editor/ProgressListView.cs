using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressListView : VisualElement
{
    private InspectorView _inspectorView;

    private string _targetGuid;
    
    private TextField _textField;
    private DropdownField _dropdownField;
    
    private List<ConditionData> _conditionDataList;
    private List<CompareElement> _compareElements = new List<CompareElement>();
    
    public ProgressListView(InspectorView inspectorView, string targetGuid, List<ConditionData> conditionDataList)
    {
        _inspectorView = inspectorView;
        _targetGuid = targetGuid;
        _conditionDataList = conditionDataList;
        InitStyle();

        GenerateToolbar();
        
        LoadConditionData();
    }

    private void InitStyle()
    {
        style.width = 250;
        style.minHeight = 100;
        style.backgroundColor = new StyleColor(Color.grey);
        style.overflow = Overflow.Hidden;
    }

    #region Toolbar
    //툴바 생성
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        _dropdownField = new DropdownField();
        _dropdownField.style.width = 90;
        toolbar.Add(_dropdownField);
        toolbar.Add(new Button(AddCondition){text = "추가"});
        
        /*//신규노드 생성 버튼
        var nodeCreateButton = new Button(() => { _graphView.CreateNode("Dialog Node"); });
        nodeCreateButton.text = "신규 노드 생성";
        toolbar.Add(nodeCreateButton);*/
        
        Add(toolbar);
    }
    private void AddCondition()
    {
        
    }
    #endregion
    
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