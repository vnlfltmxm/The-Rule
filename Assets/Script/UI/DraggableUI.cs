using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private DroppableSlotUI _previousDroppableSlot;

    public DroppableSlotUI PreviousDroppableSlot
    {
        get => _previousDroppableSlot;
        set => _previousDroppableSlot = value;
    }

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _previousDroppableSlot = transform.parent.GetComponent<DroppableSlotUI>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //transform.SetParent(UIManager.Instance.UICanvas.transform); // 부모 오브젝트를 Canvas로 설정
        transform.SetAsLastSibling(); // 가장 앞에 보이도록 마지막 자식으로 설정
        OnBeginDragCallback();
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rect.position = eventData.position;
        OnDragCallback();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject pointerEnterObject = eventData.pointerEnter;
        if (pointerEnterObject == null)
            _previousDroppableSlot.OnDrop(this);
        else if (pointerEnterObject.TryGetComponent<DroppableSlotUI>(out DroppableSlotUI droppableSlot))
            droppableSlot.OnDropDraggableUI(this);
        else if (pointerEnterObject.TryGetComponent<DraggableUI>(out DraggableUI draggableUI))
            draggableUI.PreviousDroppableSlot.OnDropDraggableUI(this);
        OnEndDragCallback();
    }

    /*private void DropDraggableUI(DroppableSlotUI droppableSlot)
    {
        DraggableUI ui = droppableSlot.DraggableUI;
        droppableSlot.OnDrop(this);
        _previousDroppableSlot.OnDrop(ui);

        DroppableSlotUI prevDropUI = _previousDroppableSlot;
        _previousDroppableSlot = droppableSlot;
        if(ui != null)
            ui.PreviousDroppableSlot = prevDropUI;
    }*/
    
    protected virtual void OnBeginDragCallback() { }
    protected virtual void OnDragCallback() { }
    protected virtual void OnEndDragCallback() { }
    
}
