using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour
{
    private RectTransform _rect;
    public RectTransform Rect
    {
        get
        {
            if(_rect == null)
            {
                _rect = GetComponent<RectTransform>();
            }
            return _rect;
        }
    }
    private DragDropUI _dragDropUI;
    public DragDropUI DragDropUI { get => _dragDropUI; set => _dragDropUI = value; }
    
    private DraggableUI _draggableUI;
    public DraggableUI DraggableUI => _draggableUI;
    //private event Action<DroppableUI, DroppableUI> OnUIDropEvent;
    
    public bool IsEmpty => _draggableUI == null;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        if (transform.childCount > 0 && transform.GetChild(0).TryGetComponent<DraggableUI>(out DraggableUI draggableUI))
        {
            _draggableUI = draggableUI;
        }
    }
    
    public void OnDropDraggableUI(DraggableUI newDragUI)
    {
        DraggableUI prevDragUI = this._draggableUI;
        DraggableUI nextDragUI = newDragUI;
        
        DroppableUI prevSlotUI = this;
        DroppableUI nextSlotUI = newDragUI.PreviousDroppable;
        
        prevSlotUI.OnDrop(newDragUI);
        nextSlotUI.OnDrop(prevDragUI);
        
        DragDropUI.SwapItem(prevSlotUI.DragDropUI, nextSlotUI.DragDropUI, prevSlotUI, nextSlotUI);
    }
    public void OnDrop(DraggableUI draggableUI)
    {
        _draggableUI = draggableUI;
        if (_draggableUI == null)
            return;
        draggableUI.PreviousDroppable = this;
        _draggableUI.transform.SetParent(transform);
        _draggableUI.Rect.position = Rect.position;
    }
}
