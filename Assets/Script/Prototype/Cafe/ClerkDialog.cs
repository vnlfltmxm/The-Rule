using UnityEngine;
using TMPro;
using System.Collections;

public class ClerkDialog : MonoBehaviour
{
    private TextMeshProUGUI _clerkDialogText;

    private void Awake()
    {
        _clerkDialogText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public IEnumerator StartDialog()
    {
        _clerkDialogText.text = string.Empty;

        string dialog = "May I take your order, sir?";

        foreach(var text in dialog)
        {
            _clerkDialogText.text += text;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}
