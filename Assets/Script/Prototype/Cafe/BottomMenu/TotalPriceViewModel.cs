using System.ComponentModel;

public class TotalPriceViewModel
{
    private int _totalPrice;

    public int TotalPrice
    {
        get { return _totalPrice; }
        set
        {
            if(value == _totalPrice)
            {
                return;
            }

            _totalPrice = value;

            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void RegisterChangeEventOnEnable(IResetMenuUI resetMenuUI)
    {
        CafeManager.Instance.RegisterBottomMenu(CalculateValue, resetMenuUI);
    }

    public void UnregisterChangeEventOnDisable()
    {
        CafeManager.Instance.UnRegisterBottomMenu(CalculateValue);
    }

    private void CalculateValue(int totalPrice)
    {
        TotalPrice += totalPrice;
    }
}
