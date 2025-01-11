using System;
using System.Collections.Generic;
using System.Threading;
using Script.Util;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ResourceManager
{
    public static T Instantiate<T>(PrefabDataType type, string name, Transform transform)
    {
        GameObject prefab = GetPrefab(type, name);
        return Object.Instantiate(prefab, transform).GetComponent<T>();
    }
    public static GameObject GetPrefab(PrefabDataType type, string name)
    {
        if (DataManager.PrefabData.Value.ContainsKey(type))
        {
            return DataManager.PrefabData.Value[type].FindPrefab(name);
        }
        else
        {
            Logger.LogError($"ResourceManager의 prefabData에 Key: {type} 가 존재하지 않습니다.");
            return null;
        }
    }
    public static ItemData GetItemData(int itemID)
    {
        if (DataManager.ItemDataBase!=null)
        {
            return DataManager.ItemDataBase.Value.GetItemData(itemID);
        }
        else
        {
            Logger.LogError($"ResourceManager의 spriteData가 존재하지 않습니다.");
            return null;
        }
    }
}
