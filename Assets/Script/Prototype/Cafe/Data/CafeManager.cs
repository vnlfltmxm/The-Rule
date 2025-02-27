using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeManager : SingletonMonoBehaviour<CafeManager>
{
    private const string _cafeDataPath = "Data/CafeData";

    private HashSet<string> _soldOutSet;
    private HashSet<string> _notRecommendSet;
    private HashSet<string> _colorlessFoodSet;

    private Dictionary<RuleHashSet, HashSet<string>> _ruleHashSetDictionary;
    private Dictionary<string, Delegate> _cafeMenuEvent = new Dictionary<string, Delegate>();
    private List<IResetMenuUI> _resetMenuList = new List<IResetMenuUI>();
    private Action<int> _cafeBottomMenuEvent;

    private CafeData _data;
    private System.Random _random;
    private Action _resetBannerMenu;

    private GameObject _playerPrefab;

    public HashSet<string> SoldOutSet => _soldOutSet;
    public HashSet<string> NotRecommendSet => _notRecommendSet;
    public HashSet<string> ColorlessFoodSet => _colorlessFoodSet;
    public CafeData Data => _data;
    public string RecommendedMenu { get; set; }
    public GameObject PlayerPrefab
    {
        get => _playerPrefab;
        set => _playerPrefab = value;
    }

    private void Awake()
    {
        _random = new System.Random();

        InitializeCafeManager();
    }

    private void InitializeCafeManager()
    {
        LoadCafeData();
        InitializeBanner();
        InitializeColorlessFoodHashSet();
        MakeRuleHashSetDictionary();
    }

    private void LoadCafeData()
    {
        var scriptableData = Resources.Load<ScriptableObject>(_cafeDataPath);

        if (scriptableData is CafeData data)
        {
            _data = data;
        }
    }

    private void InitializeColorlessFoodHashSet()
    {
        _colorlessFoodSet = _colorlessFoodSet ?? new HashSet<string>();

        foreach(var menu in _data.ColorlessFoodNames)
        {
            if (string.IsNullOrWhiteSpace(menu))
            {
                continue;
            }

            _colorlessFoodSet.Add(menu);
        }
    }

    private void MakeRuleHashSetDictionary()
    {
        _ruleHashSetDictionary = new Dictionary<RuleHashSet, HashSet<string>>
        {
            {RuleHashSet.SoldOut, _soldOutSet },
            {RuleHashSet.ColorlessFood, _colorlessFoodSet },
            {RuleHashSet.NotRecommend, _notRecommendSet }
        };
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

        string[] shuffleArray = ShuffleArray(data);

        for(int i = 0; i < 3; i++)
        {
            _soldOutSet.Add(shuffleArray[i]);
        }
        
        _notRecommendSet.UnionWith(_soldOutSet);
        //UnionWith -> �ΰ��� HashSet�� ������ �� ���. �ߺ��� ���� �����ϰ� ���ο� ���� �߰�.
    }

    private string[] ShuffleArray(CafeData data)
    {
        string[] copyArray = new string[data.CafeMenuData.Length];

        for(int i = 0; i < copyArray.Length; i++)
        {
            copyArray[i] = data.CafeMenuData[i]._menuName;
        }
        
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

        foreach(var menu in _data.CafeMenuData)
        {
            if (!_notRecommendSet.Contains(menu._menuName))
            {
                stringList.Add(menu._menuName);
            }
        }

        RecommendedMenu = stringList.Count <= 1 ?
            stringList[0] : stringList[_random.Next(stringList.Count)];
    }

    public void ResetBannerMenu() //��� �ʱ�ȭ �ڵ�
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

    #region MVVM
    public void RegisterMenu<TValue_1, TValue_2>(string key, Action<TValue_1, TValue_2> callBack, IResetMenuUI resetMenuUI)
    {
        if (!_cafeMenuEvent.ContainsKey(key))
        {
            _cafeMenuEvent.Add(key, callBack);
        }

        AddResetMenuUIList(resetMenuUI);
    }

    public void UnRegisterMenu<TValue_1, TValue_2>(string key, Action<TValue_1, TValue_2> callBack)
    {
        if (_cafeMenuEvent.ContainsKey(key))
        {
            _cafeMenuEvent[key] = Delegate.Remove(_cafeMenuEvent[key], callBack);

            if (_cafeMenuEvent[key] == null)
            {
                _cafeMenuEvent.Remove(key);
            }
        }//Delegate.Remove( ) �������� ��������Ʈ ��, Ư�� ��������Ʈ�� ������. �Ű����� (��������Ʈ ü���� ������, �����Ϸ��� ��������Ʈ)�� �ް� ���� �� ���ο� ��������Ʈ ü���� ��ȯ�Ѵ�.
        //���� ���� ������� �۵��Ͽ� ��������Ʈ ü���� ��ȯ�ϹǷ�, ��ȯ�� ��������Ʈ ü���� �ٽ� ���� ������ �Ҵ��ؾ� ��������Ʈ ü���� ���ŵȴ�.
    }

    public void TriggerMenuEvent<TValue_1, TValue_2>(string key, TValue_1 totalCount, TValue_2 totalPrice)
    {
        if (_cafeMenuEvent.ContainsKey(key))
        {
            int count = (int)(object)totalCount;

            Action<string> addAction = (count > 0) ?
                (string keyValue) => CafeRule.AddSelectedMenu(keyValue) :
                (string keyValue) => CafeRule.RemoveSelectedMenu(keyValue);

            addAction.Invoke(key);
        }

        if(_cafeMenuEvent.TryGetValue(key, out Delegate callBack))
        {
            (callBack as Action<TValue_1, TValue_2>)?.Invoke(totalCount, totalPrice);
        } //_cafeMenuEvent[key]�� �������� ���� DelegateŸ��. ��������Ʈ�� �پ��� Ÿ���� �޼��带 ������ �� �ֱ⶧���� 
        //�� Delegate�� Action<T>Ÿ���̶�� ������ ����. ���� as �����ڸ� ����Ͽ� Action<T> Ÿ������ ĳ���� �� ȣ���ϴ� ���� ������ ���.
    }

    public void RegisterBottomMenu(Action<int> callBack, IResetMenuUI resetMenuUI)
    {
        if(_cafeBottomMenuEvent == null)
        {
            _cafeBottomMenuEvent += callBack;
        }

        AddResetMenuUIList(resetMenuUI);
    }

    public void UnRegisterBottomMenu(Action<int> callBack)
    {
        if(_cafeBottomMenuEvent != null)
        {
            _cafeBottomMenuEvent -= callBack;
        }
    }

    public void TriggerBottomMenuEvent(int totalPrice)
    {
        _cafeBottomMenuEvent?.Invoke(totalPrice);
    }

    public void ResetCafeMenu()
    {
        foreach(var menuUI in _resetMenuList)
        {
            menuUI.ResetMenu();
        }
    }

    private void AddResetMenuUIList(IResetMenuUI resetMenuUI)
    {
        if (!_resetMenuList.Contains(resetMenuUI))
        {
            _resetMenuList.Add(resetMenuUI);
        }
    }

    #endregion

    #region Rule
    public void ProcessPayment()
    {
        CafeRule rule = new CafeRule(_ruleHashSetDictionary, RecommendedMenu);
    }
    #endregion
}
