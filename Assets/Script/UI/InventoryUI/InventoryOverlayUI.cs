using System;
using System.Collections.Generic;
using System.Linq;
using Script.Manager.Framework;
using Script.Pawn.Player;
using Script.Util;
using UnityEngine;

public class InventoryOverlayUI : UI_Base
{
    private List<OverlayItemUIElement> _overlayItemElements = new List<OverlayItemUIElement>();
    
    private GameObjectPool<OverlayItemUIElement> _overlayItemElementPool;
    
    public override void Init()
    {
        _overlayItemElementPool = new GameObjectPool<OverlayItemUIElement>(InstantiateUIElement);
        
        int itemCount = Player.Instance.PlayerData.InventoryData.InventorySize;

        // 아이템이 많아서 제거해야 할 경우
        if (_overlayItemElements.Count > itemCount)
        {
            // 제거해야 할 아이템 수 계산
            int itemsToRemove = _overlayItemElements.Count - itemCount;

            for (int i = 0; i < itemsToRemove; i++)
                RemoveItemFromList(_overlayItemElements.Count - 1);
        }
        // 아이템이 부족해서 추가해야 할 경우
        else if (_overlayItemElements.Count < itemCount)
        {
            // 추가해야 할 아이템 수 계산
            int itemsToAdd = itemCount - _overlayItemElements.Count;

            for (int i = 0; i < itemsToAdd; i++)
                AddItemToList();
        }
        
        int count = 0;
        foreach (var itemID in Player.Instance.PlayerData.InventoryData.ItemList)
        {
            if (itemID != Utility.Struct.IntNull)
            {
                _overlayItemElements[count].gameObject.SetActive(true);
                _overlayItemElements[count].SetItemImage(itemID);
            }
            else
                _overlayItemElements[count].gameObject.SetActive(false);
            count++;
        }
    }

    private OverlayItemUIElement InstantiateUIElement()
    {
        return ResourceManager.Instantiate<OverlayItemUIElement>(PrefabDataType.UIElement, "OverlayItemUIElement",
            transform);
    }

    // 풀에서 아이템 꺼내서 리스트에 추가 또는 새로 생성하여 추가하는 함수
    public void AddItemToList()
    {
        OverlayItemUIElement tempItem = _overlayItemElementPool.GetObject();

        // 리스트에 추가
        _overlayItemElements.Add(tempItem);
    }
    // 리스트에서 아이템 제거 함수
    public void RemoveItemFromList(int index)
    {
        if (Utility.Validation.IsIndexValid(_overlayItemElements, index))
        {
            OverlayItemUIElement itemToRemove = _overlayItemElements[index];
            _overlayItemElements.RemoveAt(index);
            _overlayItemElementPool.ReturnObject(itemToRemove);
        }
    }
}
