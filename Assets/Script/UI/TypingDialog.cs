using UnityEngine;
using TMPro;
using System.Collections;

public class TypingDialog
{
    public TypingDialog(float typingSpeed)
    {
        _typingSpeed = new WaitForSeconds(typingSpeed);
    }

    private WaitForSeconds _typingSpeed;

    public IEnumerator StartTypingDialog(string dialog, TextMeshProUGUI textMeshPro, GameObject ui = null)
    {
        if (string.IsNullOrEmpty(dialog) ||  textMeshPro == null)
            yield break;

        ToggleVisibilityGameObject(ui);

        textMeshPro.text = string.Empty;

        foreach(var text in dialog)
        {
            textMeshPro.text += text;
            yield return _typingSpeed;
        }

        yield return new WaitForSeconds(2f);
        ToggleVisibilityGameObject(ui);
    }

    private void ToggleVisibilityGameObject(GameObject ui)
    {
        if (ui == null)
            return;

        bool activeSelf = ui.activeSelf ? false : true;
        ui.SetActive(activeSelf);
    }
}
