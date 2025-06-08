using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressLine : GraphElement
{
    private StateMachineView _stateMachineView;
    private StateMachineNode _startNode;
    private StateMachineNode _endNode;
    private Vector2 _startPoint;
    private Vector2 _endPoint;

    private const float arrowLength = 10f;
    private const float arrowAngle = 30f;

    private bool _isSelected = false;
    
    public List<ConditionData> ConditionDatas = new List<ConditionData>();

    public ProgressLine(StateMachineView stateMachineView, StateMachineNode startNode)
    {
        _stateMachineView = stateMachineView;

        InitStyle();
        
        SetStartNode(startNode);
        
        // 클릭 방지 (선 위에서는 선택 X)
        //pickingMode = PickingMode.Ignore;
        pickingMode = PickingMode.Position;

        generateVisualContent += OnGenerateVisualContent;
        
        InstallManipulator();
    }

    private void InitStyle()
    {
        style.position = Position.Absolute;
        style.left = 0;
        style.top = 0;
        style.width = 1000;
        style.height = 1000;
    }
    
    public void SetStartNode(StateMachineNode startNode)
    {
        _startNode = startNode;
        _startPoint = GetPosition(_startNode);
        _startNode.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            _startPoint = GetPosition(_startNode);
            this.MarkDirtyRepaint();
        });
    }
    public void SetEndElement(StateMachineNode endNode)
    {
        _endNode = endNode;
        _endPoint = GetPosition(_endNode);
        _endNode.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            _endPoint = GetPosition(_endNode);
            this.MarkDirtyRepaint();
        });

        _startNode.StateElement.NewConditionDictionary(_endNode.StateElement.Guid);
    }
    public void SetTemporaryEnd(Vector2 mousePosition)
    {
        _endPoint = mousePosition;
        this.MarkDirtyRepaint();
    }

    private void OnGenerateVisualContent(MeshGenerationContext context)
    {
        var painter = context.painter2D;

        // 화살표 그리기
        DrawArrow(painter, _startPoint, _endPoint);
    }

    private void DrawArrow(Painter2D painter, Vector2 start, Vector2 end)
    {
        painter.strokeColor = _isSelected ? Color.cyan : Color.white;
        painter.lineWidth = 2;
        painter.BeginPath();
        painter.MoveTo(_startPoint);
        painter.LineTo(_endPoint);
        painter.Stroke();
        
        Vector2 direction = (end - start).normalized;
        Vector2 right = Quaternion.Euler(0, 0, arrowAngle) * -direction;
        Vector2 left = Quaternion.Euler(0, 0, -arrowAngle) * -direction;

        Vector2 arrowRight = (start + end) / 2 + right * arrowLength;
        Vector2 arrowLeft = (start + end) / 2 + left * arrowLength;
        Vector2 arrowTop = (start + end) / 2 + direction * arrowLength;
        Vector2 arrowBot = (start + end) / 2;
        
        painter.fillColor = _isSelected ? Color.cyan : Color.white;
        
        painter.BeginPath();
        painter.MoveTo(arrowTop);
        painter.LineTo(arrowRight);
        painter.LineTo(arrowLeft);
        painter.LineTo(arrowTop);
        painter.Fill();
    }

    #region Select
    public override void OnSelected()
    {
        base.OnSelected();
        _isSelected = true;
        this.MarkDirtyRepaint();
        Debug.LogError("OnSelected");
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        _isSelected = false;
        this.MarkDirtyRepaint();
    }
    
    public override bool ContainsPoint(Vector2 localPoint)
    {
        const float clickableWidth = 10f; // 클릭 가능한 굵기

        Vector2 dir = (_endPoint - _startPoint).normalized;
        float length = Vector2.Distance(_startPoint, _endPoint);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 클릭 영역을 회전 사각형처럼 만듦
        Matrix4x4 matrix = Matrix4x4.TRS(_startPoint, Quaternion.Euler(0, 0, angle), Vector3.one);
        Matrix4x4 inverse = matrix.inverse;

        // localPoint를 선 기준 로컬 공간으로 변환
        Vector2 local = inverse.MultiplyPoint3x4(localPoint);

        Rect clickableRect = new Rect(0, -clickableWidth / 2f, length, clickableWidth);

        return clickableRect.Contains(local);
    }
    #endregion
    
    private Vector2 GetPosition(GraphElement element)
    {
        return element.localBound.center;
        //return element.worldBound.center;// + new Vector2(-300, -50);
        //return this.WorldToLocal(element.worldBound.center);
        //return element.worldBound.center;
    }
    
    #region Manipulator
    void InstallManipulator()
    {
        var manipulator = new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("Delete", action =>
            {
                _stateMachineView.RemoveLine(this);
            }, DropdownMenuAction.AlwaysEnabled);
        });

        this.AddManipulator(manipulator); // <-- 반드시 필요!
    }
    #endregion
}
