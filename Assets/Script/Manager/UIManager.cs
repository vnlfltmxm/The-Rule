using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Canvas _uiCanvas;
    public Canvas UICanvas => _uiCanvas;

    private Dictionary<string, Transform> _uiDictionary = new Dictionary<string, Transform>();

    public void OpenUI(string uiName)
    {
        if (_uiDictionary.TryGetValue(uiName, out Transform uiTransform))
        {
            uiTransform.gameObject.SetActive(true);
        }
    }
    public T GetUIComponent<T>(string uiName) where T : MonoBehaviour
    {
        if (_uiDictionary.TryGetValue(uiName, out Transform uiTransform))
        {
            return uiTransform.GetComponent<T>();
        }
        return null;
    }
    public void CloseUI(string uiName)
    {
        if (_uiDictionary.TryGetValue(uiName, out Transform uiTransform))
        {
            uiTransform.gameObject.SetActive(false);
        }
    }
}
