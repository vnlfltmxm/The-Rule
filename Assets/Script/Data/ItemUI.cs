using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : DraggableUI
{
    [SerializeField] private Image _itemImage;

    public void SetItemImage(Sprite sprite)
    {
        if(_itemImage.sprite != sprite)
            _itemImage.sprite = sprite;
    }

    protected override void OnBeginDragCallback()
    {
        _itemImage.raycastTarget = false;
    }
    protected override void OnDragCallback()
    {
        
    }
    protected override void OnEndDragCallback()
    {
        _itemImage.raycastTarget = true;
    }
}
