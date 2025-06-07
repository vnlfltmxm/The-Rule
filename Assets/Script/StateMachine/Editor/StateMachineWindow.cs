using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineWindow : EditorWindow
{
    private VisualElement _leftPanelView;
    private GlobalFieldView _fieldView;
    private InspectorView _inspectorView;
    private StateMachineView _stateView;
    private Blackboard _blackBoard;
    
    [MenuItem("StateMachine/State Machine")]
    public static void OpenStateMachineWindow()
    {
        var window = GetWindow<StateMachineWindow>();
        window.titleContent = new GUIContent("State Machine Editor");

        StateMachineUtil.CreateStateMachineData("aa");
    }
    
    private void OnEnable()
    {
        rootVisualElement.style.flexDirection = FlexDirection.Row;
        ConstructLeftPanelView();
        ConstructFieldView();
        ConstructInspectorView();
        ConstructStateView();
    }

    private void ConstructLeftPanelView()
    {
        _leftPanelView = new VisualElement()
        {
            name = "Left Panel"
        };
        rootVisualElement.Add(_leftPanelView);
    }

    //글로벌 필드 뷰 생성
    private void ConstructFieldView()
    {
        _fieldView = new GlobalFieldView(this)
        {
            name = "Global Field"
        };
        _leftPanelView.Add(_fieldView);
    }
    
    private void ConstructInspectorView()
    {
        _inspectorView = new InspectorView(this)
        {
            name = "Inspector"
        };
        _leftPanelView.Add(_fieldView);
    }
    
    //상태머신 뷰 생성
    private void ConstructStateView()
    {
        var graphContainer = new VisualElement();
        graphContainer.style.flexGrow = 1;
        rootVisualElement.Add(graphContainer);
        _stateView = new StateMachineView(this)
        {
            name = "State Machine"
        };
        graphContainer.Add(_stateView);
    }
}