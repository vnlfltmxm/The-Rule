using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    private Dictionary<Type, UIBase> _uiDictionary = new Dictionary<Type, UIBase>();

    public T GetUI<T>() where T : UIBase
    {
        if (_uiDictionary.TryGetValue(typeof(T), out UIBase uiBase))
        {
            if(uiBase is T typeUI)
                return typeUI;
        }
        else
        {
            T typeUI = gameObject.GetComponentInChildren<T>(true);
            if (typeUI != null)
            {
                _uiDictionary.Add(typeof(T), typeUI);
                return typeUI;
            }
        }
        return null;
    }
}
