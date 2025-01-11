
public class CafeNextButton : CafeButton
{
    protected override void Awake()
    {
        base.Awake();
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
        CafeManager.Instance.TriggerMenuEvent(_key, _count, _price);
        CafeManager.Instance.TriggerBottomMenuEvent(_price);
    }

    public override void SetValue(string key, int price, int count)
    {
        _key = key;
        _price = price;
        _count = count;
    }
}
