using System;
using System.Collections.Generic;
using UnityEngine;

public class RuleContextManager : SingletonMonoBehaviour<RuleContextManager>
{
    private Dictionary<string, string> _ruleDictionary = new Dictionary<string, string>();

    public void AddRule(string name, string rule)
    {
        _ruleDictionary.Add(name, rule);
    }
    public List<string> GetStageRules()
    {
        List<string> rules = new List<string>();
        foreach (var rule in _ruleDictionary.Values)
        {
            rules.Add(rule);
        }
        return rules;
    }
}
