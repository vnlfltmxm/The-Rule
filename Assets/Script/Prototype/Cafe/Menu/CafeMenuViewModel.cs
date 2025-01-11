using System.ComponentModel;

public class CafeMenuViewModel
{
    private int _totalCount;
    private int _totalPrice;

    public int TotalCount
    {
        get { return _totalCount; }
        set
        {
            if(value == _totalCount)
            {
                return;
            }
            else if(value < 0)
            {
                return;
            }
            
            _totalCount = value;

            OnPropertyChanged(nameof(TotalCount));  
        }
    }

    public int TotalPrice
    {
        get { return _totalPrice; }
        set
        {
            if(value == _totalPrice)
            {
                return;
            }
            else if(value < 0)
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

    public void RegisterChangeEventOnEnable(string key, IResetMenuUI resetMenuUI)
    {
        CafeManager.Instance.RegisterMenu<int, int>(key, CalCulateValue, resetMenuUI);
    }

    public void UnRegisterChangeEventOnDisable(string key)
    {
        CafeManager.Instance.UnRegisterMenu<int,int>(key, CalCulateValue);
    }

    private void CalCulateValue(int totalCount, int totalPrice)
    {
        TotalCount += totalCount;
        TotalPrice += totalPrice;
    }
}
