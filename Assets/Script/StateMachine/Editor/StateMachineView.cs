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
        
        CreateStateMachineNode(typeof(IdleStateData), new Vector2(100, 200));
        CreateStateMachineNode(typeof(IdleStateData), new Vector2(100, 200));
        //CreateStateMachineNode(typeof(AreaConditionData), new Vector2(100, 200));

        RegisterMouseEvt();
    }
    
    private void InitStyle()
    {
        this.StretchToParentSize(); // 그래프뷰 안에서 요소 크기 채우도록
    }
    
    #region Node
    public StateMachineNode CreateStateMachineNode(Type stateType, Vector2 position)
    {
        var stateMachineNode = StateMachineNode.Factory(this, Guid.NewGuid(), stateType);
        
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
            // 리스트에서도 제거
            _stateMachineNodes.Remove(targetNode);
            // VisualElement에서 제거
            RemoveElement(targetNode);
        }
    }
    #endregion

    #region ProgressLine
    public void CreateLine(GraphElement startNode)//, GraphElement endNode)
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
        Debug.Log(localPos);
        if (_isDrawingProgressLine)
            _drawingLine?.SetTemporaryEnd(localPos);//evt.mousePosition - new Vector2(300, 25));
    }
    private void OnMouseDown(MouseDownEvent evt)
    {
        if (_isDrawingProgressLine && evt.button == 0)
        {
            //Vector2 localPos = evt.localMousePosition;
            VisualElement clicked = panel.Pick(evt.mousePosition); //panel.Pick(localPos);
            Debug.LogError(clicked.name);
            if (clicked is StateMachineNode node && node != _drawingStartNode)
                _drawingLine.SetEndElement(node);
            else
                RemoveLine(_drawingLine);
            UnloadLine();
        }
    }
    #endregion
}
