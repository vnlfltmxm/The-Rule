using System;
using System.Collections.Generic;
using System.Linq;
using Script.Manager.Framework;
using Script.Util;
using UnityEngine;

public class DragDropUI : MonoBehaviour
{
    [SerializeField] private List<DroppableUI> _slotList;
    [SerializeField] private int _slotCount;
    [SerializeField] private Func<DraggableUI> _draggableFactory;

    public IReadOnlyList<DroppableUI> SlotList => _slotList;

    private GameObjectPool<DraggableUI> _draggableUIPool;
    private GameObjectPool<DroppableUI> _droppableUIPool;
    private string _poolParentName = "DraggableUIPool";

    public static event Action<DragDropUI, DragDropUI, int, int> OnUIDropItemEvent;

    public int SlotCount
    {
        get => _slotCount;
        set
        {
            _slotCount = value;
            InitSlot();
        }
    }

    private void Awake()
    {
        _slotList = GetComponentsInChildren<DroppableUI>().ToList();
        //_itemList = GetComponentsInChildren<DraggableUI>().ToList();
        InitSlot();
        
        _draggableUIPool = new GameObjectPool<DraggableUI>(InstantiateDraggableUI);
        _droppableUIPool = new GameObjectPool<DroppableUI>(InstantiateDroppableUI);
    }

    private DraggableUI InstantiateDraggableUI()
    {
        return ResourceManager.Instantiate<DraggableUI>(PrefabDataType.UIElement, "ItemUIElement", transform);
    }
    private DroppableUI InstantiateDroppableUI()
    {
        return ResourceManager.Instantiate<DroppableUI>(PrefabDataType.UIElement, "DroppableUI", transform);
    }
    private void InitSlot()
    {
        if (_slotCount > _slotList.Count)
        {
            int count = _slotCount - _slotList.Count;
            for (int i = 0; i < count; i++)
            {
                DroppableUI droppableUI = _droppableUIPool.GetObject();
                droppableUI.DragDropUI = this;
                _slotList.Add(droppableUI);
            }
        }
    }
    public void AddItem(int index)
    {
        if (Utility.Validation.IsIndexValid(_slotList, index) == false)
        {
            Logger.LogError($"Item을 추가하려는 Index가 Slot밖 입니다. SlotCount: {_slotList.Count} Index: {index}");
            return;
        }
        DroppableUI droppableUI = _slotList[index];
        if (droppableUI.IsEmpty == false)
        {
            Logger.LogError($"droppableUI에 이미 DraggableUI가 존재합니다.");
            return;
        }
        DraggableUI draggableUI = _draggableUIPool.GetObject();
        if (draggableUI == null)
        {
            Logger.LogError($"DraggableUI가 생성되지 않았습니다.");
            return;
        }
        
        droppableUI.OnDrop(draggableUI);
    }
    public void RemoveItem(int index)
    {
        DroppableUI droppableUI = this.GetSlot(index);
        if (droppableUI.IsEmpty == false)
        {
            DraggableUI draggableUI = droppableUI.DraggableUI;
            Destroy(draggableUI);
            droppableUI.OnDrop(null);
        }
    }

    public static void SwapItem(DragDropUI originUI, DragDropUI targetUI, int leftItemIndex, int rightItemIndex)
    {
        
    }
    public static void SwapItem(DragDropUI originUI, DragDropUI targetUI, DroppableUI leftSlot, DroppableUI rightSlot)
    {
        int originSlotIndex = originUI.GetSlotIndex(leftSlot);
        int targetSlotIndex = targetUI.GetSlotIndex(rightSlot);
        OnUIDropItemEvent?.Invoke(originUI, targetUI, originSlotIndex, targetSlotIndex);
    }

    public int GetSlotIndex(DroppableUI droppableUI)
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (droppableUI == _slotList[i])
                return i;
        }
        return Utility.Struct.IntNull;
    }
    public DroppableUI GetSlot(int index)
    {
        return Utility.Validation.IsIndexValid(_slotList, index) ? _slotList[index] : null;
    }
    //public static DraggableUI GetItem(DragDropUI dragDropUI, int index) => Utility.Validation.IsIndexValid(dragDropUI._slotList, index) ? dragDropUI._slotList[index].DraggableUI : null;
}