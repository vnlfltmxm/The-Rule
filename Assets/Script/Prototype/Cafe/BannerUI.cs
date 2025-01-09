using System.Collections;
using UnityEngine;
using TMPro;

public class BannerUI : MonoBehaviour
{
    [Header("ClerkImage")]
    [SerializeField] private GameObject _clerkImageUI;
    private TextMeshProUGUI _clerkText;

    private void Awake()
    {
        _clerkText = _clerkImageUI.GetComponentInChildren<TextMeshProUGUI>();
    }

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
}
