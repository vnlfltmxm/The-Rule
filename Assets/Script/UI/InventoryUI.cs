using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    [SerializeField] private Transform _itemSlotParent;
    
    [SerializeField] private List<DroppableSlotUI> _slotList = new List<DroppableSlotUI>();
    
    //TODO: ResourceManager로 나중에 빼야함
    [SerializeField] private GameObject droppableSlotUIPrefab;
    

    public event Action<int, int> OnUIUpdateModelEvent;

    protected override void OnAwake()
    {
        base.OnAwake();
        RefreshItemUI();
        //Init(_inventory);
    }
    /*private void Init(InventoryBase inventory)
    {
        for (int index = 0; index < inventory.InventorySize; index++)
        {
            DroppableSlotUI droppableSlotUI;
            if (0 <= index && index < _slotList.Count)
            {
                droppableSlotUI = _slotList[index];
            }
            else
            {
                droppableSlotUI = Instantiate(droppableSlotUIPrefab, _itemSlotParent).GetComponent<DroppableSlotUI>();
                _slotList.Add(droppableSlotUI);
            }
            droppableSlotUI.SetDropEvent(OnUIUpdateModel);
        }

        _slotList[0].InitDraggableUI();
        _slotList[1].InitDraggableUI();
        _slotList[2].InitDraggableUI();
    }
    
    //데이터가 변경되어서 UI를 수정해야하는 경우
    private void OnModelUpdateUI(InventoryBase inventory)
    {
        foreach (var itemKeyValue in inventory.ItemDictionary)
        {
            if (itemKeyValue.Value == null)
            {
                _slotList[itemKeyValue.Key].ClearDraggableUI();
            }
            else
            {
                _slotList[itemKeyValue.Key].InitDraggableUI();
                if (_slotList[itemKeyValue.Key].DraggableUI is ItemUI itemUI)
                {
                    itemUI.SetItemImage(itemKeyValue.Value.Sprite);
                }
            }
        }
    }

    //UI가 수정되어서 데이터를 변경해야하는경우
    private void OnUIUpdateModel(DroppableSlotUI leftSlot, DroppableSlotUI rightSlot)
    {
        int leftIndex = _slotList.IndexOf(leftSlot);
        int rightIndex = _slotList.IndexOf(rightSlot);
        OnUIUpdateModelEvent?.Invoke(leftIndex, rightIndex);
    }
    */
    
    private void RefreshItemUI()
    {
        //InventoryManager.Instance.Items
    }
    
    private void OnEnable()
    {
        //_inventory.OnModelUpdateUIEvent += OnModelUpdateUI;
        InventoryManager.Instance.OnInventoryDataChangeEvent += RefreshItemUI;
    }
    private void OnDisable()
    {
        //_inventory.OnModelUpdateUIEvent -= OnModelUpdateUI;
        InventoryManager.Instance.OnInventoryDataChangeEvent -= RefreshItemUI;
    }
}
