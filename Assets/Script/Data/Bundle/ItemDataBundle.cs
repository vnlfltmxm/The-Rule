
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBundle", menuName = "Bundle/ItemDataBundle")]
public class ItemDataBundle : ScriptableObject
{
    [SerializeField] private List<ItemData> _itemDatas = new List<ItemData>();
    
    public ItemData GetItemData(string dataID)
    {
        foreach (var itemData in _itemDatas)
        {
            if (itemData.Name == dataID.ToString())
                return itemData;
        }
        return null;
    }

    public void LoadData()
    {
        // "Resources/Data/Item" 폴더 내 모든 파일 로드
        _itemDatas.Clear();
        ItemData[] items = Resources.LoadAll<ItemData>("Data/Item"); 
        foreach (var item in items)
        {
            _itemDatas.Add(item);
        }
        Logger.Log("로드 완료");
    }
}