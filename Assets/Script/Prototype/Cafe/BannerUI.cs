using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class BannerUI : MonoBehaviour
{
    [Header("ClerkImage")]
    [SerializeField] private GameObject _clerkImageUI;
    private TextMeshProUGUI _clerkText;

    [Header("RecommendMenuText")]
    [SerializeField] private TextMeshProUGUI _recommendText;

    [Header("SoldOutText")]
    [SerializeField] private TextMeshProUGUI[] _soldOutTextArray;

    private void Awake()
    {
        _clerkText = _clerkImageUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        RegisterMethod();
    }

    private void OnDisable()
    {
        UnregisterMethod();
    }

    private void Start()
    {
        InitializeRecommendText();
        InitializeSoldOutText();
    }

    #region Dialog
    public IEnumerator StartClerkDialog()
    {
        ActiveClerkImageUI(true);

        _clerkText.text = string.Empty;

        string dialog = "주문하시겠습니까?";

        foreach(var text in dialog)
        {
            _clerkText.text += text;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        ActiveClerkImageUI(false);
    }

    private void ActiveClerkImageUI(bool isActive)
    {
        _clerkImageUI.SetActive(isActive);
    }
    #endregion

    private void InitializeRecommendText()
    {
        _recommendText.text = string.Empty;

        _recommendText.text = CafeManager.Instance.RecommendedMenu;
    }

    private void InitializeSoldOutText()
    {
        var soldOutSet = CafeManager.Instance.SoldOutSet;

        var soldOutArray = soldOutSet.ToArray();

        for(int i = 0; i < soldOutArray.Length; i++)
        {
            if (!string.IsNullOrEmpty(soldOutArray[i]))
            {
                _soldOutTextArray[i].text = string.Empty;

                _soldOutTextArray[i].text = soldOutArray[i];    
            }
        }
    }

    private void RegisterMethod()
    {
        CafeManager.Instance.RegisterBanner(InitializeRecommendText, true);
        CafeManager.Instance.RegisterBanner(InitializeSoldOutText, true);
    }

    private void UnregisterMethod()
    {
        CafeManager.Instance.RegisterBanner(InitializeRecommendText, false);
        CafeManager.Instance.RegisterBanner(InitializeSoldOutText, false);
    }
}
