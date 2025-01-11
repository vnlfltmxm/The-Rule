using System;
using System.Linq;
using System.Collections.Generic;
using Script.Manager.Framework;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public InventoryData InventoryData { get; private set; } = new InventoryData();
    
    public event Action OnUpdateDataEvent;

    public void AddItem(int itemID)
    {
        InventoryData.AddItem(itemID);
        OnUpdateDataEvent?.Invoke();
    }
}

[Serializable]
public class InventoryData
{
    public List<int> ItemList { get; private set; } = new List<int>();
    public int InventorySize { get; private set; } = 10;
    public int CurrentInventoryCount => ItemList.Count(n => n != int.MinValue);

    public InventoryData()
    {
        for (int slotIndex = 0; slotIndex < InventorySize; slotIndex++)
        {
            ItemList.Add(Utility.Struct.IntNull);
        }
    }
    public bool AddItem(int itemID)
    {
        for (int slotIndex = 0; slotIndex < InventorySize; slotIndex++)
        {
            // 키의 값이 null인 경우에만 추가
            if (ItemList[slotIndex] == Utility.Struct.IntNull)
            {
                AddItem(slotIndex, itemID);
                return true;
            }
        }
        return false;
    }
    private void AddItem(int slotIndex, int itemID)
    {
        ItemList[slotIndex] = itemID;
    }
    public void RemoveItem(int slotIndex)
    {
        ItemList.Remove(slotIndex);
    }
    public void SwapItem(int leftItemIndex, int rightItemIndex)
    {
        (ItemList[leftItemIndex], ItemList[rightItemIndex]) = (ItemList[rightItemIndex], ItemList[leftItemIndex]);
    }
    public static void SwapItem(InventoryData leftInventoryData, int leftItemIndex, InventoryData rightInventoryData, int rightItemIndex)
    {
        (leftInventoryData.ItemList[leftItemIndex], rightInventoryData.ItemList[rightItemIndex]) 
            = (rightInventoryData.ItemList[rightItemIndex], leftInventoryData.ItemList[leftItemIndex]);
    }
}