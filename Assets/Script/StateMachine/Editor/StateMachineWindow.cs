using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineWindow : EditorWindow
{
    private VisualElement _leftPanelView;
    public GlobalFieldView FieldView;
    public InspectorView InspectorView;
    public StateMachineView StateView;
    
    //[MenuItem("StateMachine/State Machine")]
    public static void OpenStateMachineWindow(StateMachine stateMachine, GameObject owner)
    {
        var window = GetWindow<StateMachineWindow>();
        window.titleContent = new GUIContent("State Machine Editor");

        if (stateMachine.StateMachineData == null)
            stateMachine.StateMachineData = StateMachineUtil.CreateStateMachineData($"{owner.name}State");
        else
            StateMachineUtil.StateMachineData = stateMachine.StateMachineData;

        window.LoadStateMachineWindow();
    }
    
    private void LoadStateMachineWindow()
    {
        rootVisualElement.style.flexDirection = FlexDirection.Row;
        ConstructLeftPanelView();
        ConstructFieldView();
        ConstructInspectorView();
        ConstructStateView();

        FieldView.LoadData();
        StateView.LoadData();
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
        FieldView = new GlobalFieldView(this)
        {
            name = "Global Field"
        };
        _leftPanelView.Add(FieldView);
    }
    
    private void ConstructInspectorView()
    {
        InspectorView = new InspectorView(this)
        {
            name = "Inspector"
        };
        _leftPanelView.Add(FieldView);
    }
    
    //상태머신 뷰 생성
    private void ConstructStateView()
    {
        var graphContainer = new VisualElement();
        graphContainer.style.flexGrow = 1;
        rootVisualElement.Add(graphContainer);
        StateView = new StateMachineView(this)
        {
            name = "State Machine"
        };
        graphContainer.Add(StateView);
    }
}