using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableSlotUI : MonoBehaviour
{
    //TODO: ResourceManager로 나중에 빼야함
    [SerializeField] private GameObject draggableUIPrefab;
    
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
    [SerializeField] private DraggableUI _draggableUI;
    public DraggableUI DraggableUI => _draggableUI;
    
    private event Action<DroppableSlotUI, DroppableSlotUI> OnUIDropEvent;
    
    public bool IsEmpty => _draggableUI == null;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        if (transform.childCount > 0 && transform.GetChild(0).TryGetComponent<DraggableUI>(out DraggableUI draggableUI))
        {
            _draggableUI = draggableUI;
        }
    }

    public void SetDropEvent(Action<DroppableSlotUI, DroppableSlotUI> dropEvent)
    {
        OnUIDropEvent = dropEvent;
    }
    
    public void OnDropDraggableUI(DraggableUI draggableUI)
    {
        //내가 넣을곳의 Drop이다!
        DraggableUI dragUI = this._draggableUI;
        this.OnDrop(draggableUI);
        draggableUI.PreviousDroppableSlot.OnDrop(dragUI);

        DroppableSlotUI prevDropUI = draggableUI.PreviousDroppableSlot;
        draggableUI.PreviousDroppableSlot = this;
        if(dragUI != null)
            dragUI.PreviousDroppableSlot = prevDropUI;
    }
    public void OnDrop(DraggableUI draggableUI)
    {
        _draggableUI = draggableUI;
        if (_draggableUI == null)
            return;
        _draggableUI.transform.SetParent(transform);
        _draggableUI.Rect.position = Rect.position;
    }

    //TODO: 이거 나중에 오브젝트 풀링 씀
    public void InitDraggableUI()
    {
        if(IsEmpty)
            _draggableUI = Instantiate(draggableUIPrefab, transform).GetComponent<DraggableUI>();
    }
    public void ClearDraggableUI()
    {
        if(!IsEmpty)
            Destroy(_draggableUI);
    }
}
