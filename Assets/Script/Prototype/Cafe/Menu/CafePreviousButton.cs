using UnityEngine;
using UnityEngine.UI;

public class CafePreviousButton : CafeButton
{
    [Header("Alpha")]
    [SerializeField] private float _disableAlpha;
    [SerializeField] private float _enableAlpha;

    public float DisableAlpha => _disableAlpha;
    public float EnableAlpha => _enableAlpha;
    public bool IsEnabled => _button.enabled;
    
    private Image _buttonImage;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _buttonImage = gameObject.GetComponent<Image>();

        SetAlpha(_disableAlpha, false);
    }

    private void OnEnable()
    {
        OnEnableButton();
    }

    private void OnDisable()
    {
        OnDisableButton();
    }

    public override void OnClickEvent()
    {
        CafeManager.Instance.TriggerMenuEvent(_key, -_count, -_price);
        CafeManager.Instance.TriggerBottomMenuEvent(-_price);
    }

    public override void SetValue(string key, int price, int count)
    {
        _key = key;
        _price = price;
        _count = count;
    }

    public void SetAlpha(float targetAlpha, bool isEnable)
    {
        Color color = _buttonImage.color;

        color.a = targetAlpha;

        _buttonImage.color = color;

        _button.enabled = isEnable;
    }
}
