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
    Live
}

public enum RuleHashSet
{
    SoldOut,
    NotRecommend,
    ColorlessFood
}

public class CafeRule
{
    private static Dictionary<string, int> _selectedMenuDictionary
        = new Dictionary<string, int>();

    private Dictionary<RuleHashSet, HashSet<string>> _ruleSetDictionary;

    public static bool _blackTeaconcentrate;
    public static bool _isOtherconcentrate;

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
        var soldOutSet = GetRuleHashSet(RuleHashSet.SoldOut);
        var colorlessFoodSet = GetRuleHashSet(RuleHashSet.ColorlessFood);
        var notRecommendSet = GetRuleHashSet(RuleHashSet.NotRecommend);

        bool isDeath = Death(colorlessFoodSet, soldOutSet);

        if (isDeath)
            return Rule.Death;

        bool isItem = IsItemRule();

        if (isItem)
            return Rule.IsItemRule;

        bool isColorlessFoodRule = ColorlessFoodRule(colorlessFoodSet);

        if (isColorlessFoodRule)
            return Rule.ColorlessFoodRule;

        bool isSoldOutRule = SoldOutRule(soldOutSet);

        if (isSoldOutRule)
            return Rule.SoldOutRule;

        bool isRecommend = RecommendedRule(_currentRecommendedMenu);

        if (isRecommend)
            return Rule.RecommendedRule;

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
            case RuleHashSet.NotRecommend:
                hashset = CafeManager.Instance.NotRecommendSet;
                return hashset;
        }

        return null;
    }

    private bool IsItemRule()
    {
        return IsItem();
    }

    private bool ColorlessFoodRule(HashSet<string> colorlessFoodSet)
    {
        return ContainsHashSetItem(colorlessFoodSet);
    }

    private bool SoldOutRule(HashSet<string> soldOutSet)
    {
        return ContainsHashSetItem(soldOutSet);
    }

    private bool RecommendedRule(string recommendedMenu)
    {
        return _selectedMenuDictionary.ContainsKey(recommendedMenu);
    }

    private bool Death(HashSet<string> colorlessFoodSet, HashSet<string> soldOutSet)
    {
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
        return true;
    }

    private bool IsItemOrderHistory(string menuName)
    {
        return _selectedMenuDictionary.ContainsKey(menuName)
            && _selectedMenuDictionary[menuName] > 0;
    }

    private bool IsItem()
    {
        return _selectedMenuDictionary.Count > 0;
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
}
