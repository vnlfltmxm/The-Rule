using UnityEngine;
using UnityEngine.UI;

public abstract class CafeButton : MonoBehaviour
{
    protected Button _button;

    protected string _key;
    protected int _count;
    protected int _price;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }

    public abstract void OnClickEvent();

    public abstract void SetValue(string key, int price, int count);

    protected void OnEnableButton()
    {
        _button.onClick.AddListener(OnClickEvent);
    }

    protected void OnDisableButton()
    {
        _button.onClick.RemoveListener(OnClickEvent);
    }
}
