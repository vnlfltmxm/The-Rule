using UnityEngine;
using UnityEngine.UI;

public class ItemBase
{
    private ItemData _itemData;

    public int ID => _itemData.ID;
    public string Name => _itemData.Name;
    public Sprite Sprite => _itemData.Sprite;

    public ItemBase(ItemData itemData)
    {
        _itemData = itemData;
    }
}
