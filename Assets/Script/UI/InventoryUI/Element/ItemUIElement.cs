using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIElement : MonoBehaviour
{
    [SerializeField] private Image _itemImage;

    public void SetItemImage(int itemID)
    {
        if (_itemImage == null)
            _itemImage = GetComponent<Image>();

        Sprite sprite = ResourceManager.GetItemData(itemID).Sprite;
        
        if(_itemImage.sprite != sprite)
            _itemImage.sprite = sprite;
    }

    public void OnBeginDragCallback()
    {
        _itemImage.raycastTarget = false;
    }
    /*public void OnDragCallback()
    {
        
    }*/
    public void OnEndDragCallback()
    {
        _itemImage.raycastTarget = true;
    }
}
