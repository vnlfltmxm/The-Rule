using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : SingletonMonoBehaviour<DialogManager>
{
    private TypingDialog _typingDialog;
    
    public IEnumerator TypingDialog(string text, TextMeshProUGUI textMeshProUGUI,
        GameObject ui = null)
    {
        if (_typingDialog == null)
            _typingDialog = new TypingDialog(0.1f);

        yield return StartCoroutine(_typingDialog.StartTypingDialog(text, textMeshProUGUI, ui));
    }
}
