using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private DroppableUI _previousDroppable;

    public DroppableUI PreviousDroppable
    {
        get => _previousDroppable;
        set => _previousDroppable = value;
    }

    public UnityEvent OnDragEvent;
    public UnityEvent OnDropEvent;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _previousDroppable = transform.parent.GetComponent<DroppableUI>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //transform.SetParent(UIManager.Instance.UICanvas.transform); // 부모 오브젝트를 Canvas로 설정
        transform.SetAsLastSibling(); // 가장 앞에 보이도록 마지막 자식으로 설정
        OnDragEvent.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rect.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject pointerEnterObject = eventData.pointerEnter;
        if (pointerEnterObject == null)
            _previousDroppable.OnDrop(this);
        else if (pointerEnterObject.TryGetComponent<DroppableUI>(out DroppableUI droppableSlot))
            droppableSlot.OnDropDraggableUI(this);
        else if (pointerEnterObject.TryGetComponent<DraggableUI>(out DraggableUI draggableUI))
            draggableUI.PreviousDroppable.OnDropDraggableUI(this);
        OnDropEvent.Invoke();
    }
}
