using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    private List<ItemData> _items = new List<ItemData>();
    public IReadOnlyList<ItemData> Items => _items;

    private int _inventorySize;
    public int InventorySize => _inventorySize;
    
    public event Action OnInventoryDataChangeEvent;

    protected override void OnAwake()
    {
        base.OnAwake();
        InitInventory();
    }

    public void InitInventory()
    {
        
    }
    public void AddItem(string itemID)
    {
        ItemData itemData = DataManager.Instance.GetItemData(itemID);
        AddItem(itemData);
    }
    public void AddItem(ItemData itemData)
    {
        _items.Add(itemData);
        OnInventoryDataChangeEvent?.Invoke();
    }
    public void RemoveItem(string itemID)
    {
        ItemData tempItemData = FindItem(itemID);
        if(tempItemData != null)
            RemoveItem(tempItemData);
    }
    public void RemoveItem(ItemData itemData)
    {
        _items.Remove(itemData);
        OnInventoryDataChangeEvent?.Invoke();
    }

    public ItemData FindItem(string itemID)
    {
        foreach (var itemData in Items)
            if (itemData.ID == itemID)
                return itemData;
        
        return null;
    }
    public ItemData FindItem(ItemData itemData)
    {
        return FindItem(itemData.ID);
    }
}
