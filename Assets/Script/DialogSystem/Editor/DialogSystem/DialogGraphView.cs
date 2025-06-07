using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogGraphView : GraphView
{
    public readonly Vector2 DefaultNodeSize = new Vector2(150, 200);

    public Blackboard Blackboard;
    public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
    private NodeSearchWindow _searchWindow;
    
    public DialogGraphView(EditorWindow editorWindow)
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
        
        AddElement(GenerateEntryPointNode());
        AddSearchWindow(editorWindow);
    }

    private void AddSearchWindow(EditorWindow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.Init(editorWindow, this);
        nodeCreationRequest = context =>
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
    }


    

    #region Node
    //진입 시점 생성
    private DialogNode GenerateEntryPointNode()
    {
        var node = new DialogNode()
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            DialogText = "ENTRY_POINT",
            EntryPoint = true,
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);
        
        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName, Vector2 position)
    {
        AddElement(CreateDialogNode(nodeName, position));
    }

    //dialog노드 생성
    public DialogNode CreateDialogNode(string nodeName, Vector2 position)
    {
        var dialogNode = new DialogNode()
        {
            title = nodeName,
            GUID = Guid.NewGuid().ToString(),
            DialogText = nodeName,
        };

        var inputPort = GeneratePort(dialogNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogNode.inputContainer.Add(inputPort);

        var addButton = new Button(() => { AddChoicePort(dialogNode); });
        addButton.text = "Add";
        dialogNode.titleContainer.Add(addButton);
        
        //var deleteButton = new Button(() => { DeleteChoicePort(dialogNode); });
        //deleteButton.text = "Delete";
        //dialogNode.titleContainer.Add(deleteButton);

        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt =>
        {
            dialogNode.DialogText = evt.newValue;
            dialogNode.title = evt.newValue;
        });
        textField.SetValueWithoutNotify(dialogNode.title);
        dialogNode.mainContainer.Add(textField);
        
        dialogNode.RefreshExpandedState();
        dialogNode.RefreshPorts();
        
        dialogNode.SetPosition(new Rect(position, DefaultNodeSize));
        return dialogNode;
    }
    #endregion

    #region Port
    //포트 연결?
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach((port) =>
        {
            if(startPort!=port && startPort.node!=port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    //연결점 생성
    private Port GeneratePort(DialogNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }
    public void AddChoicePort(DialogNode dialogNode, string overridePortName = "")
    {
        // 포트를 감싸는 컨테이너 생성
        //VisualElement portContainer = new VisualElement();
        //portContainer.style.flexDirection = FlexDirection.Row; // 가로 정렬
        //portContainer.style.alignItems = Align.Center; // 정렬
        
        var generatedPort = GeneratePort(dialogNode, Direction.Output);
        
        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        oldLabel.style.display = DisplayStyle.None;
        
        var outputPortCount = dialogNode.outputContainer.Query("connector").ToList().Count;

        var choicePortName = string.IsNullOrEmpty(overridePortName) 
                ? $"Choice {outputPortCount + 1}" 
                : overridePortName;
        
        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName,
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        
        var deleteButton = new Button(() => RemovePort(dialogNode, generatedPort))
        {
            text = "X",
        };
        
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        generatedPort.contentContainer.Add(deleteButton);
        
        //portContainer.contentContainer.Add(new Label("  "));
        //portContainer.contentContainer.Add(textField);
        //portContainer.contentContainer.Add(deleteButton);
        //
        //dialogNode.outputContainer.Add(portContainer);

        generatedPort.pickingMode = PickingMode.Position;
        generatedPort.portName = choicePortName;
        
        dialogNode.outputContainer.Add(generatedPort);
        
        dialogNode.RefreshPorts();
        dialogNode.RefreshExpandedState();
    }

    private void RemovePort(DialogNode dialogNode, Port generatedPort)
    {
        var targetEdge = edges.ToList().Where(x =>
            x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        dialogNode.outputContainer.Remove(generatedPort);
        dialogNode.RefreshPorts();
        dialogNode.RefreshExpandedState();
    }
    #endregion

    #region BlackBoard
    public void AddPropertyToBlackBoard(ExposedProperty exposedProperty)
    {
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue;
        while (ExposedProperties.Any(x=>x.PropertyName == localPropertyName))
        {
            localPropertyName = $"{localPropertyName}(1)";
        }
        
        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;
        ExposedProperties.Add(property);

        var container = new VisualElement();
        var blackboardField = new BlackboardField { text = property.PropertyName, typeText = "string"};
        container.Add(blackboardField);

        var propertyValueTextField = new TextField("Value:")
        {
            value = localPropertyValue
        };
        propertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
            ExposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
        });
        var blackBoardValueRow = new BlackboardRow(blackboardField, propertyValueTextField);
        container.Add(blackBoardValueRow);
        
        Blackboard.Add(container);
    }
    
    public void ClearBlackBoardAndExposedProperties()
    {
        ExposedProperties.Clear();
        Blackboard.Clear();
    }
    #endregion
}
