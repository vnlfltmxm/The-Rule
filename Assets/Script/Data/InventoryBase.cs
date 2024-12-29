using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
    private Dictionary<int, ItemBase> _itemDictionary = new Dictionary<int, ItemBase>();
    public IReadOnlyDictionary<int, ItemBase> ItemDictionary => _itemDictionary;

    [SerializeField] private int _inventorySize = 10;
    public int InventorySize => _inventorySize;

    //TODO: 추후 UI매니저로 가져올것
    [SerializeField] private InventoryUI _inventoryUI;
    
    public event Action<InventoryBase> OnModelUpdateUIEvent;
    private void Awake()
    {
        InitInventoryData();
        
    }

    private void InitInventoryData()
    {
        for (int index = 0; index < _inventorySize; index++)
        {
            _itemDictionary.Add(index, null);
        }
        _itemDictionary[0] = new ItemBase(new ItemData());
        _itemDictionary[1] = new ItemBase(new ItemData());
        _itemDictionary[2] = new ItemBase(new ItemData());
    }

    public void OnUISwapItem(int leftItemIndex, int rightItemIndex)
    {
        _itemDictionary.TryAdd(leftItemIndex, null);
        _itemDictionary.TryAdd(rightItemIndex, null);
        (_itemDictionary[leftItemIndex], _itemDictionary[rightItemIndex]) = (_itemDictionary[rightItemIndex], _itemDictionary[leftItemIndex]);
        //OnModelUpdateUIEvent?.Invoke(this);
    }
    public bool OnModelAddItem(ItemBase itemBase)
    {
        for (int index = 0; index < _inventorySize; index++)
        {
            // 키가 없거나 해당 키의 값이 null인 경우에만 추가
            if (!_itemDictionary.ContainsKey(index) || _itemDictionary[index] == null)
            {
                AddItem(itemBase, index);
                return true;
            }
        }
        return false;
    }
    private void AddItem(ItemBase itemBase, int itemIndex)
    {
        _itemDictionary[itemIndex] = itemBase;
        OnModelUpdateUIEvent?.Invoke(this);
    }
    public void RemoveItem(int itemIndex)
    {
        _itemDictionary.Remove(itemIndex);
        OnModelUpdateUIEvent?.Invoke(this);
    }

    private void OnEnable()
    {
        _inventoryUI.OnUIUpdateModelEvent += OnUISwapItem;
    }
    private void OnDisable()
    {
        _inventoryUI.OnUIUpdateModelEvent -= OnUISwapItem;
    }
}
