using UnityEngine;


[CreateAssetMenu(fileName = "CafeDialog", menuName = "Scriptable Objects/CafeDialog")]
public class CafeDialog : ScriptableObject
{
    [SerializeField] private string _onMenuUI;
    public string OnMenuUI => _onMenuUI;

    [SerializeField] private string _selectUI;
    public string SelectUI => _selectUI;
}
