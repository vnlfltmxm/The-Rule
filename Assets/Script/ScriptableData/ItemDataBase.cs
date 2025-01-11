using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDataBase : ScriptableObject
{
    [field: SerializeField]
    public List<ItemData> ItemDatas { get; private set; }

    public ItemData GetItemData(int itemID)
    {
        foreach (var itemData in ItemDatas)
        {
            if (itemData.ID == itemID)
                return itemData;
        }
        Logger.LogError($"{itemID}에 해당하는 ItemData가 ItemDataBase에 존재하지 않습니다.");
        return null;
    }
}