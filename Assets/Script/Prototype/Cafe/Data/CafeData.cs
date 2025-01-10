using UnityEngine;

[CreateAssetMenu(fileName = "CafeData", menuName = "Scriptable Objects/CafeData")]
public class CafeData : ScriptableObject
{
    [Header("Menu")]
    [SerializeField] private string[] _menuData;

    [Header("NotRecommendMenu")]
    [SerializeField] private string[] _notRecommendMenu;

    public string[] MenuData => _menuData;
    public string[] NotRecommendMenu => _notRecommendMenu;
}
