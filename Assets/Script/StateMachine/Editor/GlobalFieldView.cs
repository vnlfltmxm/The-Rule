using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalFieldView : VisualElement
{
    private StateMachineWindow _stateMachineWindow;
    private TextField _textField;
    private DropdownField _dropdownField;
    private ScrollView _scrollView;
    private List<GlobalFieldElement> _fieldElements = new List<GlobalFieldElement>();
    public GlobalFieldView(StateMachineWindow stateMachineWindow)
    {
        _stateMachineWindow = stateMachineWindow;
        InitStyle();
        GenerateToolbar();
        GenerateScrollArea(); // 추가
    }

    private void InitStyle()
    {
        style.width = 300;
        style.flexShrink = 0; // 고정 크기 유지
        style.flexGrow = 0;
        style.flexBasis = 300;
        style.backgroundColor = new StyleColor(Color.gray);
        style.height = Length.Percent(100); // 세로 방향 100%
        style.overflow = Overflow.Hidden;
    }
    #region Toolbar
    //툴바 생성
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        _textField = new TextField();
        _textField.style.width = 150;
        toolbar.Add(_textField);
        
        List<string> dropdownData = new List<string>();
        
        var types = TypeCache.GetTypesWithAttribute<GlobalFieldTypeAttribute>();
        foreach (var type in types)
        {
            if (!type.IsClass || type.IsAbstract) continue;

            var attr = type.GetCustomAttribute<GlobalFieldTypeAttribute>();
            if (attr == null) continue;

            if (!dropdownData.Contains(attr.Name))
                dropdownData.Add(attr.Name);
        }
        
        _dropdownField = new DropdownField(dropdownData, dropdownData.First());
        _dropdownField.style.width = 90;
        toolbar.Add(_dropdownField);
        toolbar.Add(new Button(AddGlobalField){text = "추가"});
        
        /*//신규노드 생성 버튼
        var nodeCreateButton = new Button(() => { _graphView.CreateNode("Dialog Node"); });
        nodeCreateButton.text = "신규 노드 생성";
        toolbar.Add(nodeCreateButton);*/
        
        Add(toolbar);
    }
    #endregion
    
    private void GenerateScrollArea()
    {
        _scrollView = new ScrollView(ScrollViewMode.Vertical);
        _scrollView.style.flexGrow = 1; // 스크롤 영역이 아래 공간을 모두 차지하게 함
        _scrollView.style.overflow = Overflow.Visible;
        Add(_scrollView);
    }
    
    private void AddGlobalField()
    {
        if (string.IsNullOrWhiteSpace(_textField.value))
        {
            EditorUtility.DisplayDialog("필드명 오류", "글로벌 필드명이 비어있습니다", "확인");
        }
        else if(ContainsDuplicateName(_textField.value))
        {
            EditorUtility.DisplayDialog("필드명 중복", "글로벌 필드명이 이미 존재합니다", "확인");
        }
        else
        {
            GlobalFieldElement globalFieldElement = new GlobalFieldElement(this, _textField.value, _dropdownField.value);
            _fieldElements.Add(globalFieldElement);
            _scrollView.Add(globalFieldElement);
        }
    }

    private bool ContainsDuplicateName(string elementName)
    {
        foreach (var globalFieldElement in _fieldElements)
        {
            if (globalFieldElement.ElementName == elementName)
                return true;
        }
        return false;
    }

    public void RemoveElement(GlobalFieldElement globalFieldElement)
    {
        var findElement = _fieldElements.Find(x => x == globalFieldElement);
        _fieldElements.Remove(findElement);
        _scrollView.Remove(findElement);
    }
}
