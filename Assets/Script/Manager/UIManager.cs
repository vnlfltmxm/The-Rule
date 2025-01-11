using System;
using System.Collections.Generic;
using System.Threading;
using Script.Manager.Framework;
using Script.Util;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    
    private static Lazy<Canvas> SceneCanvas = new Lazy<Canvas>(()=>UIManager.Instance.GetCanvas("SceneCanvas"), LazyThreadSafetyMode.None);
    private static Lazy<Canvas> PopupCanvas = new Lazy<Canvas>(()=>UIManager.Instance.GetCanvas("PopupCanvas"), LazyThreadSafetyMode.None);

    private static Canvas _tempCanvas;
    
    //Stack<UI_Base> _popupUIStack = new Stack<UI_Base>();
    Dictionary<string, UI_Base> _allPopupUI = new Dictionary<string, UI_Base>();

    protected override void Init()
    {
        _tempCanvas = FindFirstObjectByType<Canvas>();
        UI_Base[] uiBases = _tempCanvas.GetComponentsInChildren<UI_Base>(true);
        foreach (var uiBase in uiBases)
        {
            string uiFullName = (uiBase.name == uiBase.GetType().Name) ? uiBase.name : $"{uiBase.GetType().Name}_{uiBase.name}";
            _allPopupUI.Add(uiFullName, uiBase);
        }
    }

    public Canvas GetCanvas(string name)
    {
        GameObject canvasObject = GameObject.Find(name);
        if (canvasObject == null)
        {
            GameObject prefab = ResourceManager.GetPrefab(PrefabDataType.UI, name);
            if (prefab == null)
            {
                Debug.LogError($"Prefab for {name} not found!");
                return null;
            }
            canvasObject = Object.Instantiate(prefab);
            canvasObject.name = name;
        }
        return canvasObject.GetComponent<Canvas>();
    }

    public T OpenUI<T>(string uiName = null) where T : UI_Base
    {
        T ui = FindUI<T>(uiName);
        ui?.gameObject.SetActive(true);
        return ui;
    }
    public T CloseUI<T>(string uiName = null) where T : UI_Base
    {
        T ui = FindUI<T>(uiName);
        ui?.gameObject.SetActive(false);
        return ui;
    }
    public T GetUIComponent<T>(string uiName = null) where T : MonoBehaviour
    {
        return FindUI<UI_Base>(uiName)?.GetComponent<T>();
    }

    public T FindUI<T>(string uiName = null) where T : UI_Base
    {
        string uiFullName = (uiName == null) ? typeof(T).Name : $"{typeof(T).Name}_{uiName}";
        
        if (_allPopupUI.ContainsKey(uiFullName) == false)
        {
            Logger.Log($"UI가 존재하지 않음, UI 오브젝트 생성중: {uiFullName}");
            GameObject prefab = ResourceManager.GetPrefab(PrefabDataType.UI, uiName);
            if (prefab == null)
            {
                Logger.LogError($"UI 프리팹을 받아오지 못했습니다: {uiFullName}");
                return null;
            }
            GameObject uiObject = Object.Instantiate(prefab, PopupCanvas.Value.transform);
            uiObject.name = uiFullName;
            _allPopupUI.Add(uiFullName, uiObject.GetComponent<UI_Base>());
        }
        return (T)_allPopupUI[uiFullName];
    }
}