using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineView : GraphView
{
    private StateMachineWindow _stateMachineWindow;
    public readonly Vector2 DefaultNodeSize = new Vector2(150, 100);
    
    private List<StateMachineNode> _stateMachineNodes = new List<StateMachineNode>();
    private List<ProgressLine> _progressLines = new List<ProgressLine>();

    private GraphElement _drawingStartNode;
    private ProgressLine _drawingLine;
    private bool _isDrawingProgressLine = false;
    
    public StateMachineView(StateMachineWindow stateMachineWindow)
    {
        _stateMachineWindow = stateMachineWindow;
        
        InitStyle();
        
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        
        Insert(0, grid);
        grid.StretchToParentSize();
        
        //CreateStateMachineNode(typeof(IdleState), new Vector2(100, 200));
        //CreateStateMachineNode(typeof(AreaConditionData), new Vector2(100, 200));

        RegisterMouseEvt();
        InstallManipulator();
    }
    
    private void InitStyle()
    {
        this.StretchToParentSize(); // 그래프뷰 안에서 요소 크기 채우도록
    }

    public void LoadData()
    {
        ClearView();
        if(StateMachineUtil.StateMachineData == null)
        {
            Debug.LogError($"StateMachineData가 로드되지 않았습니다");
            return;
        }
        //모든 노드를 생성
        foreach (var stateElement in StateMachineUtil.StateMachineData.StateElements)
            LoadStateMachineNode(stateElement);
        
        //모든 노드의 ProgressLine 생성
        foreach (var stateElement in StateMachineUtil.StateMachineData.StateElements)
        {
            foreach (var targetGuidCondition in stateElement.TargetGuidConditionDictionary)
            {
                string startNodeGuid = stateElement.Guid;
                string endNodeGuid = targetGuidCondition.Key;
                
                StateMachineNode startNode = _stateMachineNodes.FirstOrDefault(x => x.StateElement.Guid == startNodeGuid);
                StateMachineNode endNode = _stateMachineNodes.FirstOrDefault(x => x.StateElement.Guid == endNodeGuid);

                if (startNode == null)
                    Debug.LogError($"초기화 도중 startNode 를 찾지 못했습니다, startNodeGuid:{startNodeGuid}, endNodeGuid:{endNodeGuid}");
                if (endNode == null)
                    Debug.LogError($"초기화 도중 endNode 를 찾지 못했습니다, startNodeGuid:{startNodeGuid}, endNodeGuid:{endNodeGuid}");
                
                CreateCompleteLine(startNode, endNode);
            }
        }
    }
    
    #region Node
    public StateMachineNode LoadStateMachineNode(StateElement stateElement)
    {
        var stateMachineNode = new StateMachineNode(this, stateElement);
        
        stateMachineNode.SetPosition(new Rect(stateElement.NodePosition, DefaultNodeSize));
        _stateMachineNodes.Add(stateMachineNode);
        AddElement(stateMachineNode);
        return stateMachineNode;
    }
    public StateMachineNode CreateStateMachineNode(Type stateType, Vector2 position)
    {
        Guid guid = Guid.NewGuid();
        var stateElement = (StateElement)Activator.CreateInstance(stateType);
        stateElement.Guid = guid.ToString();
        stateElement.NodePosition = position;
        StateMachineUtil.StateMachineData.StateElements.Add(stateElement);
        var stateMachineNode = new StateMachineNode(this, stateElement);
        
        stateMachineNode.SetPosition(new Rect(position, DefaultNodeSize));
        _stateMachineNodes.Add(stateMachineNode);
        AddElement(stateMachineNode);
        return stateMachineNode;
    }

    public void DeleteNode(StateMachineNode node)
    {
        StateMachineNode targetNode = _stateMachineNodes.FirstOrDefault(x => x == node);
        
        if (targetNode != null)
        {
            StateMachineUtil.StateMachineData.StateElements.Remove(targetNode.StateElement);
            // 리스트에서도 제거
            _stateMachineNodes.Remove(targetNode);
            // VisualElement에서 제거
            RemoveElement(targetNode);
        }
    }
    #endregion

    #region ProgressLine
    public void CreateCompleteLine(StateMachineNode startNode, StateMachineNode endNode)//, GraphElement endNode)
    {
        _drawingLine = new ProgressLine(this, startNode);
        _progressLines.Add(_drawingLine);
        AddElement(_drawingLine);
        _drawingLine.SendToBack();
        _drawingLine.SetEndElement(endNode);
    }
    public void CreateLine(StateMachineNode startNode)//, GraphElement endNode)
    {
        _drawingStartNode = startNode;
        _isDrawingProgressLine = true;
        
        _drawingLine = new ProgressLine(this, startNode);
        
        _progressLines.Add(_drawingLine);
        AddElement(_drawingLine);
        _drawingLine.SendToBack();
    }
    public void RemoveLine(ProgressLine progressLine)
    {
        _progressLines.Remove(progressLine);
        RemoveElement(progressLine);
    }

    public void UnloadLine()
    {
        _drawingStartNode = null;
        _drawingLine = null;
        _isDrawingProgressLine = false;
    }

    private void RegisterMouseEvt()
    {
        // GraphView가 아닌 contentViewContainer에 등록해야 동작함!
        this.focusable = true;
        this.pickingMode = PickingMode.Position;
        
        this.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        this.RegisterCallback<MouseDownEvent>(OnMouseDown, TrickleDown.TrickleDown);
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        //Vector2 graphPos = contentViewContainer.WorldToLocal(evt.mousePosition);
        Vector2 localPos = evt.localMousePosition;
        if (_isDrawingProgressLine)
            _drawingLine?.SetTemporaryEnd(localPos);//evt.mousePosition - new Vector2(300, 25));
    }
    private void OnMouseDown(MouseDownEvent evt)
    {
        if (_isDrawingProgressLine && evt.button == 0)
        {
            //Vector2 localPos = evt.localMousePosition;
            VisualElement clicked = panel.Pick(evt.mousePosition); //panel.Pick(localPos);
            if (clicked is StateMachineNode node && node != _drawingStartNode)
                _drawingLine.SetEndElement(node);
            else
                RemoveLine(_drawingLine);
            UnloadLine();
        }
    }
    #endregion

    public void OnClickNode(StateMachineNode stateMachineNode)
    {
        _stateMachineWindow.InspectorView.LoadNodeData(stateMachineNode);
    }
    
    
    #region Manipulator
    void InstallManipulator()
    {
        var types = TypeCache.GetTypesWithAttribute<StateElementTypeAttribute>();
        foreach (var type in types)
        {
            var drawManipulator = new ContextualMenuManipulator(evt =>
            {
                evt.menu.AppendAction(type.ToString(), action =>
                    {
                        CreateStateMachineNode(type, evt.localMousePosition);
                    },
                    DropdownMenuAction.AlwaysEnabled);
            });
            this.AddManipulator(drawManipulator);
        }
        
        
        /*var drawManipulator = new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("IdleState", action =>
                {
                    CreateStateMachineNode(typeof(IdleState), evt.localMousePosition);
                },
                DropdownMenuAction.AlwaysEnabled);
        });

        var deleteManipulator = new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("AttackState", action =>
                {
                    CreateStateMachineNode(typeof(AttackState), evt.localMousePosition);
                },
                DropdownMenuAction.AlwaysEnabled);
        });
        
        this.AddManipulator(drawManipulator); // <-- 반드시 필요!
        this.AddManipulator(deleteManipulator); // <-- 반드시 필요!*/
    }
    #endregion

    private void ClearView()
    {
        foreach (var stateMachineNode in _stateMachineNodes)
            RemoveElement(stateMachineNode);
        _stateMachineNodes.Clear();

        foreach (var progressLine in _progressLines)
            RemoveElement(progressLine);
        _progressLines.Clear();
    }
}
