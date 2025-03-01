using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class CafeBottomMenu : MonoBehaviour, IResetMenuUI
{
    [Header("TotalPriceText")]
    [SerializeField] private TextMeshProUGUI _totalPriceText;

    [Header("BuyButton")]
    [SerializeField] private Button _buyButton;

    [Header("CancelButton")]
    [SerializeField] private Button _cancelButton;

    private TotalPriceViewModel _totalPriceViewModel;
    private Image _buyButtonImage;

    private float _disableBuyButtonAlpha = 0.2f;
    private float _enableBuyButtonAlpha = 1f;

    private void Awake()
    {
        InitializeCafeBottonMenuUI();
    }

    private void InitializeCafeBottonMenuUI()
    {
        _buyButtonImage = _buyButton.gameObject.GetComponent<Image>();
        SetAlpha(_disableBuyButtonAlpha, false);
        _totalPriceText.text = "0";
    }

    private void OnEnable()
    {
        RegisterEvent();
    }

    private void OnDisable()
    {
        UnRegisterEvent();
    }

    private void ChangedTotalPriceText(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
        _totalPriceText.text = _totalPriceViewModel.TotalPrice.ToString();

        if(_totalPriceViewModel.TotalPrice > 0)
        {
            SetAlpha(_enableBuyButtonAlpha, true);
        }
        else
        {
            SetAlpha(_disableBuyButtonAlpha, false);
        }
    }

    private void RegisterEvent()
    {
        if (_totalPriceViewModel == null)
        {
            _totalPriceViewModel = new TotalPriceViewModel();
        }

        _totalPriceViewModel.PropertyChanged += ChangedTotalPriceText;

        _totalPriceViewModel.RegisterChangeEventOnEnable(this);
    }

    private void UnRegisterEvent()
    {
        _totalPriceViewModel.PropertyChanged -= ChangedTotalPriceText;

        _totalPriceViewModel.UnregisterChangeEventOnDisable();
    }

    private void SetAlpha(float targetAlpha, bool isEnable)
    {
        Color color = _buyButtonImage.color;

        color.a = targetAlpha;

        _buyButtonImage.color = color;

        _buyButton.enabled = isEnable;
    }

    public void ResetMenu()
    {
        _totalPriceViewModel.TotalPrice = 0;
    }
}
