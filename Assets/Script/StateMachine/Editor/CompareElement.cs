using System;
using System.Collections.Generic;
using System.Linq;
using Script.Util;
using UnityEngine.UIElements;

public class CompareElement : VisualElement
{
    private ProgressListView _progressListView;
    public ConditionData ConditionData;

    private DropdownField _globalDropdownField;
    private DropdownField _compareDropdownField;
    private VisualElement _targetValueField;
    
    private float[] _elementsWidth = new[] { 0.2f, 0.3f, 0.38f, 0.1f };
    
    public CompareElement(ProgressListView progressListView, ConditionData conditionData)
    {
        _progressListView = progressListView;
        ConditionData = conditionData;
        
        InitStyle();
        InitElement();
    }
    private void InitStyle()
    {
        style.flexDirection = FlexDirection.Row;
    }

    private void InitElement()
    {
        Add(CreateGlobalFieldElement());
        Add(CreateCompareElement());
        Add(CreateTargetElement());
    }

    private DropdownField CreateGlobalFieldElement()
    {
        //모든 글로벌 필드를 드롭다운 필드에 추가
        
        // = StateMachineUtil.StateMachineData.GlobalFields..ToList();
        List<string> options = StateMachineUtil.StateMachineData.GlobalFields.Select(globalField => globalField.Guid).ToList();
        _globalDropdownField = new DropdownField(options, options.First());
        return _globalDropdownField;
    }
    private DropdownField CreateCompareElement()
    {
        //모든 비교 연산자를 추가
        _compareDropdownField = GlobalFieldUtil.CreateCompareDropdownFieldForType(ConditionData);
        return _compareDropdownField;
    }
    private VisualElement CreateTargetElement()
    {
        //조건에 따라 필드를 생성
        _targetValueField = GlobalFieldUtil.CreateTargetFieldForType(ConditionData);
        return _targetValueField;
    }
}