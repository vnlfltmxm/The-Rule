using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalFieldElement : VisualElement
{
    private GlobalFieldView _globalFieldView;
    public GlobalField GlobalField { get; private set; }
    private float[] _elementsWidth = new[] { 0.2f, 0.3f, 0.38f, 0.1f };
    public GlobalFieldElement(GlobalFieldView globalFieldView, GlobalField globalField)//string elementName, string type)
    {
        _globalFieldView = globalFieldView;
        GlobalField = globalField;
        
        InitStyle();
        InitElement();
    }
    private void InitStyle()
    {
        /*style.width = 200;
        style.flexShrink = 0; // 고정 크기 유지
        style.flexGrow = 0;
        style.flexBasis = 200;
        style.height = Length.Percent(100); // 세로 방향 100%
        style.backgroundColor = new StyleColor(Color.gray);*/
        style.flexDirection = FlexDirection.Row;
    }

    private void InitElement()
    {
        Add(CreateNameElement());
        Add(CreateTypeElement());
        Add(CreateFieldElement());
        Add(CreateButtonElement());
    }

    #region CreateElement
    private VisualElement CreateNameElement()
    {
        Label label = new Label(GlobalField.GlobalFieldName);
        label.style.width = _globalFieldView.layout.width * _elementsWidth[0];
        return label;
    }
    private VisualElement CreateTypeElement()
    {
        Label label = new Label(GlobalField.GetType().ToString());
        label.style.width = _globalFieldView.layout.width * _elementsWidth[1];
        return label;
    }
    private VisualElement CreateFieldElement()
    {
        VisualElement field = GlobalFieldUtil.CreateGlobalFieldForType(GlobalField.GetType());
        field.style.width = _globalFieldView.layout.width * _elementsWidth[2];
        return field;
    }
    private VisualElement CreateButtonElement()
    {
        Button button = new Button(RemoveSelf) { text = "X" };
        button.style.width = _globalFieldView.layout.width * _elementsWidth[3];
        return button;
    }
    #endregion

    private void RemoveSelf()
    {
        _globalFieldView.RemoveElement(this);
    }
}
