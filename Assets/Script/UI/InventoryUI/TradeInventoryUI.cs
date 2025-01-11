using System;
using System.Collections.Generic;
using System.Linq;
using Script.Manager.Framework;
using Script.Pawn.Player;
using Script.Prop;
using Script.Util;
using UnityEngine;
using UnityEngine.Serialization;

public class TradeInventoryUI : UI_Base
{
    //private ItemInventory _inventory;

    private InventoryData _playerInventory;
    private InventoryData _targetInventory;
    [SerializeField] private DragDropUI _playerDragDropUI;
    [SerializeField] private DragDropUI _targetDragDropUI;

    
    
    public override void Init()
    {
        DragDropUI.OnUIDropItemEvent += OnUIDrop;
        InitInventory(_playerDragDropUI, Player.Instance.PlayerData.InventoryData);
    }

    public void Init(InventoryData playerInventory, InventoryData targetInventory)
    {
        _playerInventory = playerInventory;
        _targetInventory = targetInventory;
        InitInventory(_playerDragDropUI, _playerInventory);
        InitInventory(_targetDragDropUI, _targetInventory);
    }
    public void InitInventory(DragDropUI dragDropUI, InventoryData inventory)
    {
        if (dragDropUI == null)
            return;
        
        dragDropUI.SlotCount = inventory.InventorySize;

        for (int index = 0; index < inventory.InventorySize; index++)
        {
            // 아이템이 존재하는 경우와 없는 경우를 처리
            if (inventory.ItemList[index] != Utility.Struct.IntNull)
                HandleItemInSlot(dragDropUI, index, inventory.ItemList[index]);
            else
                RemoveItemFromSlot(dragDropUI, index);
        }
    }
    private void HandleItemInSlot(DragDropUI dragDropUI, int slotIndex, int itemID)
    {
        DroppableUI droppableUI = dragDropUI.GetSlot(slotIndex);

        // Draggable이 비어있으면 아이템 추가
        if (droppableUI.IsEmpty)
            dragDropUI.AddItem(slotIndex);
        

        // Draggable UI 업데이트 (아이템이 다르면 이미지 변경)
        UpdateItemImage(droppableUI.DraggableUI, itemID);
    }
    private void UpdateItemImage(DraggableUI draggableUI, int itemID)
    {
        ItemUIElement itemUIElement = draggableUI.GetComponent<ItemUIElement>();
        itemUIElement.SetItemImage(itemID);
    }
    private void RemoveItemFromSlot(DragDropUI dragDropUI, int index)
    {
        // Draggable이 있으면 제거
        if (!dragDropUI.SlotList[index].IsEmpty)
        {
            dragDropUI.RemoveItem(index);
        }
    }

    public InventoryData GetUIInventoryData(DragDropUI dragDropUI)
    {
        if (dragDropUI == _playerDragDropUI)
        {
            return _playerInventory;
        }
        else if (dragDropUI == _targetDragDropUI)
        {
            return _targetInventory;
        }
        return null;
    }

    private void OnUIDrop(DragDropUI originUI, DragDropUI targetUI, int leftSlot, int rightSlot)
    {
        InventoryData leftInventoryData = GetUIInventoryData(originUI);
        InventoryData rightInventoryData = GetUIInventoryData(targetUI);
        InventoryData.SwapItem(leftInventoryData, leftSlot, rightInventoryData, rightSlot);
        Debug.Log("aa");
    }
}
