using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class CafeMenu : MonoBehaviour, IResetMenuUI
{
    [Header("FoodImage")]
    [SerializeField] private Image _foodImage;

    [Header("FoodNameText")]
    [SerializeField] private TextMeshProUGUI _foodNameText;

    [Header("FoodPriceText")]
    [SerializeField] private TextMeshProUGUI _foodPriceText;

    [Header("TotalCountText")]
    [SerializeField] private TextMeshProUGUI _totalCountText;

    [Header("TotalPriceText")]
    [SerializeField] private TextMeshProUGUI _totalPriceText;

    [Header("Button")]
    [SerializeField] private CafeNextButton _nextButton;
    [SerializeField] private CafePreviousButton _previousButton;

    private CafeMenuViewModel _menuViewModel;
    private CafeMenuData _data;

    private void OnEnable()
    {
        if(_menuViewModel != null)
        {
            RegisterEvent();
        }
    }

    private void OnDisable()
    {
        if(_menuViewModel != null)
        {
            UnRegisterEvent();
        }
    }

    public void InitializeView(CafeMenuData cafeMenuData)
    {
        _data = cafeMenuData;

        InitializeViewModel();
        InitializeButton(cafeMenuData);

        _foodImage.sprite = cafeMenuData._foodSprite;
        _foodNameText.text = cafeMenuData._menuName;
        _foodPriceText.text = cafeMenuData._price.ToString();

        _totalCountText.text = "0";
        _totalPriceText.text = "0";
    }

    private void InitializeViewModel()
    {
        _menuViewModel = new CafeMenuViewModel();

        RegisterEvent();
    }

    private void InitializeButton(CafeMenuData cafeMenuData)
    {
        _nextButton.SetValue(cafeMenuData._menuName, cafeMenuData._price, 1);
        _previousButton.SetValue(cafeMenuData._menuName, cafeMenuData._price, 1);
    }

    private void ChangeCafeMenu(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
        switch(propertyChangedEventArgs.PropertyName)
        {
            case nameof(_menuViewModel.TotalCount):
                _totalCountText.text = _menuViewModel.TotalCount.ToString();

                if(_menuViewModel.TotalCount <= 0)
                {
                    _previousButton.SetAlpha(_previousButton.DisableAlpha, false);
                }
                else
                {
                    if (!_previousButton.IsEnabled)
                    {
                        _previousButton.SetAlpha(_previousButton.EnableAlpha, true);
                    }
                }

                break;
            case nameof(_menuViewModel.TotalPrice):
                _totalPriceText.text = _menuViewModel.TotalPrice.ToString();
                break;
        }
    }

    private void RegisterEvent()
    {
        _menuViewModel.PropertyChanged -= ChangeCafeMenu;

        _menuViewModel.PropertyChanged += ChangeCafeMenu;

        _menuViewModel.RegisterChangeEventOnEnable(_data._menuName, this);
    }

    private void UnRegisterEvent()
    {
        _menuViewModel.PropertyChanged -= ChangeCafeMenu;

        _menuViewModel.UnRegisterChangeEventOnDisable(_data._menuName);
    }

    public void ResetMenu()
    {
        _menuViewModel.TotalCount = 0;
        _menuViewModel.TotalPrice = 0;
    }
}
