using System;
using System.Collections.Generic;
using Script.Util;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private ItemDataBundle _itemDataBundle;

    public DataManager()
    {
        //리소스 로드
        ResourceManager.Instance.Load<ItemDataBundle>("Data/Bundle/ItemDataBundle");
    }

    public ItemData GetItemData(string itemID)
    {
        return _itemDataBundle.GetItemData(itemID);
    }
    public ItemData Get(string itemID)
    {
        //return _itemDataBundle.GetItemData(itemID);
        return null;
    }
}
