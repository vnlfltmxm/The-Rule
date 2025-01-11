using System;
using System.Collections.Generic;
using System.Threading;
using Script.Manager.Framework;
using Script.Util;
using UnityEngine;

public static class DataManager
{
    public static readonly Lazy<Dictionary<PrefabDataType, PrefabDataBase>> PrefabData = 
        new Lazy<Dictionary<PrefabDataType, PrefabDataBase>>(InitPrefabDataBase, LazyThreadSafetyMode.None);
    public static readonly Lazy<ItemDataBase> ItemDataBase = 
        new Lazy<ItemDataBase>(InitItemDataBase, LazyThreadSafetyMode.None);

    #region 데이터 초기화 함수
    private static Dictionary<PrefabDataType, PrefabDataBase> InitPrefabDataBase()
    {
        Dictionary<PrefabDataType, PrefabDataBase> data = new Dictionary<PrefabDataType, PrefabDataBase>();
        data.Add(PrefabDataType.Unit, Resources.Load<PrefabDataBase>(Utility.Path.GetPrefabDataPath(PrefabDataType.Unit)));
        data.Add(PrefabDataType.UI, Resources.Load<PrefabDataBase>(Utility.Path.GetPrefabDataPath(PrefabDataType.UI)));
        data.Add(PrefabDataType.UIElement, Resources.Load<PrefabDataBase>(Utility.Path.GetPrefabDataPath(PrefabDataType.UIElement)));
        data.Add(PrefabDataType.Item, Resources.Load<PrefabDataBase>(Utility.Path.GetPrefabDataPath(PrefabDataType.Item)));
        return data;
    }
    private static ItemDataBase InitItemDataBase()
    {
        ItemDataBase data = Resources.Load<ItemDataBase>(Utility.Path.ItemDataBasePath);
        return data;
    }
    #endregion
}

