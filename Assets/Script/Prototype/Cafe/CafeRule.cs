using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

//ī���� ��Ģ
//���Խ� �׻� �ϳ� �̻��� ������ �����Ұ�
//��ä��(�Ͼ�, ����)�� �ƴ� ������ ��Ű�� ����
//ǰ�� ������ ��Ű�� ����
//��õ ������ ��Ű�� ����
//���� ��� ��ä���� ������ ǰ���̶�� ȫ���� �ֹ��Ұ�
//�̶� �������� ��Ḧ ���� �����Ѵ� ���Ұ�
//���� ��� ��ä���� ���İ� ȫ���� ǰ���̶��...���̾��� ���̽� ���

//������ ���� �ʰ� ������ ���-> ��Ȯ���� �̶����� ���԰�ü(������)�� �߰��� ���۵Ǹ� �� �߰��� ���� ��������
//ä����迡 ��ȣ�ۿ�� ����HP�� �ִ� HP20%�� �����ϰ� ȫ������ ������ ����
//ȫ������ ���� ȫ�� �ֹ��� ���
//�÷��̾ ������ ������ ��Ű���� ȫ���ܿ� ��������� ���� �ҽ� ���
//���ø� ��� ��������� �����ϰ� Ŀ��, ȫ���� ��Ű�� ���
//��ũ�� ����ũ, Ŀ�Ǹ� ���ѵ� ���
//ȫ�������� ������ ȫ���� ���Ѿ� ����
//�÷��̾ �����ݾ��� ���ڶ�� ����

public enum Rule
{
    IsItemRule,
    ColorlessFoodRule,
    SoldOutRule,
    RecommendedRule,
    Death,
    BalckTeaRule,
    Live
}

public enum RuleHashSet
{
    SoldOut,
    ColorlessFood
}

public class CafeRule : IDisposable
{
    private static Dictionary<string, int> _selectedMenuDictionary
        = new Dictionary<string, int>();

    private Dictionary<RuleHashSet, HashSet<string>> _ruleSetDictionary;
    
    public static bool _isBlackTeaConcentrate;
    public static bool _isConcentrate;
    private string _currentRecommendedMenu;

    public CafeRule(Dictionary<RuleHashSet, HashSet<string>> setDictionary,
        string recommendedMenu)
    {
        _ruleSetDictionary = setDictionary;
        _currentRecommendedMenu = recommendedMenu;
    }

    public static void AddSelectedMenu(string menuName)
    {
        if(_selectedMenuDictionary.ContainsKey(menuName))
        {
            _selectedMenuDictionary[menuName]++;
        }
        else
        {
            _selectedMenuDictionary.Add(menuName, 1);   
        }
    }

    public static void RemoveSelectedMenu(string menuName)
    {
        if (!_selectedMenuDictionary.ContainsKey(menuName))
        {
            return;
        }

        if (_selectedMenuDictionary[menuName] > 0)
        {
            _selectedMenuDictionary[menuName]--;
        }
        else
        {
            _selectedMenuDictionary.Remove(menuName);
        }
    }

    public Rule ProcessPayment()
    {
        var ruleList = new List<(Rule, Func<bool>)>
        {
            {(Rule.Death, Death) },
            {(Rule.IsItemRule, IsItemRule) },
            {(Rule.ColorlessFoodRule, ColorlessFoodRule) },
            {(Rule.SoldOutRule, SoldOutRule) },
            {(Rule.RecommendedRule, RecommendedRule) },
        };

        if (_isConcentrate)
        {
            ruleList.Add((Rule.BalckTeaRule, BlackTeaRule));
        }

        foreach(var(rule, ruleCheck) in ruleList)
        {
            if (ruleCheck())
            {
                return rule;
            }
        }

        return Rule.Live;
    }

    private HashSet<string> GetRuleHashSet(RuleHashSet ruleHashSet)
    {
        if (_ruleSetDictionary.TryGetValue(ruleHashSet, out HashSet<string> set))
            return set;

        HashSet<string> hashset = null;

        switch (ruleHashSet)
        {
            case RuleHashSet.SoldOut:
                hashset = CafeManager.Instance.SoldOutSet;
                return hashset;
            case RuleHashSet.ColorlessFood:
                hashset = CafeManager.Instance.ColorlessFoodSet;
                return hashset;
        }

        return null;
    }

    private bool IsItemRule()
    {
        return IsItem();
    }

    private bool ColorlessFoodRule()
    {
        var colorlessFoodSet = GetRuleHashSet(RuleHashSet.ColorlessFood);
        //�� ���� �����ؾ���
        return ContainsHashSetItem(colorlessFoodSet);
    }

    private bool SoldOutRule()
    {
        var soldOutSet = GetRuleHashSet(RuleHashSet.SoldOut);

        return ContainsHashSetItem(soldOutSet);
    }

    private bool RecommendedRule()
    {
        return _selectedMenuDictionary.ContainsKey(_currentRecommendedMenu);
    }

    private bool Death()
    {
        var colorlessFoodSet = GetRuleHashSet(RuleHashSet.ColorlessFood);
        var soldOutSet = GetRuleHashSet(RuleHashSet.SoldOut);

        bool isSubset = soldOutSet.IsSubsetOf(colorlessFoodSet);

        if (isSubset)
        {
            string menuName = "ȫ��";

            bool isContains = soldOutSet.Contains(menuName);

            if (isContains)
            {
                return true;
            }
        }

        return false;
    }

    private bool BlackTeaRule()
    {
        bool blackTeaRule = _selectedMenuDictionary.Count == 1 &&
            _selectedMenuDictionary.ContainsKey("ȫ��");

        if (blackTeaRule)
        {
            blackTeaRule = _isBlackTeaConcentrate ? true : false;

            return !blackTeaRule;
        }

        return !blackTeaRule;
    }

    private bool IsItemOrderHistory(string menuName)
    {
        return _selectedMenuDictionary.ContainsKey(menuName)
            && _selectedMenuDictionary[menuName] > 0;
    }

    private bool IsItem()
    {
        return _selectedMenuDictionary.Count <= 0;
    }

    private bool ContainsHashSetItem(HashSet<string> hashSet)
    {
        foreach(var item in hashSet)
        {
            if (IsItemOrderHistory(item))
            {
                return true;
            }
        }

        return false;
    }

    public void Dispose()
    {
        _isConcentrate = false;
        _isBlackTeaConcentrate = false;
    }
}

