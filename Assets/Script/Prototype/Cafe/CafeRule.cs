using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CafeRule
{
    private Dictionary<string, int> _selectedMenuDictionary;
    private CafeData _data;

    public CafeRule(CafeData cafeData)
    {
        _data = cafeData;

        _selectedMenuDictionary = new Dictionary<string, int>();
    }

    public void AddSelectedMenu(string menuName)
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

    public void RemoveSelectedMenu(string menuName)
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

    public bool FirstRule()
    {
        return IsItem();
    }

    public bool SecondRule(HashSet<string> colorlessFoodSet)
    {
        foreach(var menu in colorlessFoodSet)
        {
            if (IsItemOrderHistory(menu))
            {
                return true;
            }
        }

        return false;
    }

    public bool ThirdRule(HashSet<string> soldOutSet)
    {
        foreach (var menu in soldOutSet)
        {
            if (IsItemOrderHistory(menu))
            {
                return true;
            }
        }

        return false;
    }

    public bool FourthRule(string recommendedMenu)
    {
        return _selectedMenuDictionary.ContainsKey(recommendedMenu);
    }

    public bool FifthRule(HashSet<string> colorlessFoodSet, HashSet<string> soldOutSet)
    {
        bool isSubset = soldOutSet.IsSubsetOf(colorlessFoodSet);

        if (isSubset)
        {
            string menuName = "È«Â÷";

            return !(_selectedMenuDictionary.ContainsKey(menuName));
        }

        return false;
    }

    public void SixthRule()
    {

    }

    public bool SeventhRule(HashSet<string> colorlessFoodSet, HashSet<string> soldOutSet)
    {
        bool isSubset = soldOutSet.IsSubsetOf(colorlessFoodSet);

        if (isSubset)
        {
            string menuName = "È«Â÷";

            bool isContains = soldOutSet.Contains(menuName);

            if (isContains)
            {
                return true;
            }
        }

        return false;
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
}
