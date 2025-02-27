using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

//카페의 규칙
//진입시 항상 하나 이상의 음식을 구매할것
//무채색(하양, 검정)이 아닌 음식을 시키지 말것
//품절 음식을 시키지 말것
//추천 음식을 시키지 말것
//만약 모든 무채색의 음식이 품절이라면 홍차를 주문할것
//이때 점원에게 재료를 직접 제공한다 말할것
//만약 모든 무채색의 음식과 홍차가 품절이라면...운이없는 케이스 사망

//물건을 사지 않고 나가면 사망-> 정확히는 이때부터 난입개체(역무원)의 추격이 시작되며 이 추격은 벗어 날수없다
//채혈기계에 상호작용시 현재HP에 최대 HP20%를 제거하고 홍차원액 아이템 제공
//홍차원액 없이 홍차 주문시 사망
//플레이어가 정답인 음식을 시키더라도 홍차외에 재료제공에 동의 할시 사망
//예시를 들면 재료제공에 동의하고 커피, 홍차를 시키면 사망
//생크림 케이크, 커피를 시켜도 사망
//홍차원액을 가지고 홍차만 시켜야 생존
//플레이어가 결제금액이 모자라면 죽임

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
            string menuName = "홍차";

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
