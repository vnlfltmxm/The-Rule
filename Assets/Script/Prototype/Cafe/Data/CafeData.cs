using UnityEngine;

[System.Serializable]
public class CafeMenuData
{
    [Header("MenuName")]
    [SerializeField] public string _menuName;
    [Header("Price")]
    [SerializeField] public int _price;
    [Header("FoodSprite")]
    [SerializeField] public Sprite _foodSprite;
}

[CreateAssetMenu(fileName = "CafeData", menuName = "Scriptable Objects/CafeData")]
public class CafeData : ScriptableObject
{
    [Header("CafeMenuData")]
    [SerializeField] private CafeMenuData[] _cafeMenuData;

    [Header("NotRecommendMenu")]
    [SerializeField] private string[] _notRecommendMenu;

    public CafeMenuData[] CafeMenuData => _cafeMenuData;
    public string[] NotRecommendMenu => _notRecommendMenu;
}
