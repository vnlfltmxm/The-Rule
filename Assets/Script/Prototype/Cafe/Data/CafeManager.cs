using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeManager : SingletonMonoBehaviour<CafeManager>
{
    private const string _cafeDataPath = "Data/CafeData";

    private HashSet<string> _soldOutSet;
    private HashSet<string> _notRecommendSet;
    private CafeData _data;
    private System.Random _random;
    private Action _resetBannerMenu;

    public HashSet<string> SoldOutSet => _soldOutSet;
    public HashSet<string> NotRecommendSet => _notRecommendSet;
    public string RecommendedMenu { get; set; }

    private void Awake()
    {
        _random = new System.Random();

        InitializeCafeManager();
    }

    private void InitializeCafeManager()
    {
        LoadCafeData();

        InitializeBanner();
    }

    private void LoadCafeData()
    {
        var scriptableData = Resources.Load<ScriptableObject>(_cafeDataPath);

        if (scriptableData is CafeData data)
        {
            _data = data;
        }
    }

    #region Banner
    private void InitializeBanner()
    {
        InitializeNotRecommendedHashSet();

        SetSoldOutMenu(_data);

        SetRecommendedMenu();
    }

    private void InitializeNotRecommendedHashSet()
    {
        _notRecommendSet = _notRecommendSet ?? new HashSet<string>();

        _notRecommendSet.Clear();

        foreach (var menu in _data.NotRecommendMenu)
        {
            _notRecommendSet.Add(menu);
        }
    }

    private void SetSoldOutMenu(CafeData data)
    {
        _soldOutSet = _soldOutSet ?? new HashSet<string>();

        _soldOutSet.Clear();

        string[] shuffleArray = ShuffleArray(data.MenuData);

        for(int i = 0; i < 3; i++)
        {
            _soldOutSet.Add(shuffleArray[i]);
        }
        
        _notRecommendSet.UnionWith(_soldOutSet);
        //UnionWith -> 두개의 HashSet을 병합할 때 사용. 중복된 값은 무시하고 새로운 값만 추가.
    }

    private string[] ShuffleArray(string[] menuDataArray)
    {
        string[] copyArray = new string[menuDataArray.Length];

        menuDataArray.CopyTo(copyArray, 0);

        for (int i = copyArray.Length - 1; i > 0; i--)
        {
            int randomIndex = _random.Next(i + 1);

            string tempText = copyArray[i];

            copyArray[i] = copyArray[randomIndex];

            copyArray[randomIndex] = tempText;
        }

        return copyArray;
    }

    private void SetRecommendedMenu()
    {
        List<string> stringList = new List<string>();

        foreach(var menu in _data.MenuData)
        {
            if (!_notRecommendSet.Contains(menu))
            {
                stringList.Add(menu);
            }
        }

        RecommendedMenu = stringList.Count <= 1 ?
            stringList[0] : stringList[_random.Next(stringList.Count)];
    }

    public void ResetBannerMenu()
    {
        InitializeBanner();

        _resetBannerMenu?.Invoke();
    }

    public void RegisterBanner(Action action, bool register)
    {
        if (register)
        {
            _resetBannerMenu += action;
        }
        else
        {
            _resetBannerMenu -= action;
        }
    }
    #endregion
}
