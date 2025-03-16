using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : UIBase
{
    private float textUIOffset = 2f;
    [SerializeField] private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
    public void ShowText(Transform speaker, string context)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(speaker.position + Vector3.up * textUIOffset);
        _texts[0].transform.position = screenPos;
        _texts[0].gameObject.SetActive(screenPos.z > 0);

        _texts[0].text = context;
    }
}
