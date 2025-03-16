using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuleUI : UIBase
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    public void InitRule()
    {
        List<string> rules = RuleContextManager.Instance.GetStageRules();
        string context = "";
        foreach (var rule in rules)
        {
            context += $"{rule}\n";
        }

        _textMeshProUGUI.text = context;
    }
}
