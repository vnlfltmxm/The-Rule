using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DeathCafe
{
    public class CafeDialogUI : MonoBehaviour
    {
        [Header("Dialog")]
        [SerializeField] private GameObject _dialogImage;
        [SerializeField] private TextMeshProUGUI _dialogText;

        public CafeDialog DialogData { get; set; }
        
        protected IEnumerator CafeDialog(string dialogText, Action action = null)
        {
            var dialogManager = DialogManager.Instance;

            yield return StartCoroutine(dialogManager.TypingDialog(dialogText, _dialogText, _dialogImage));
            action?.Invoke();
        }
    }
}

