using UnityEngine;

public class CafeMenuPanel : MonoBehaviour
{
    [Header("MenuPanel")]
    [SerializeField] private CafeMenu[] _menuPanel;

    private void Awake()
    {
        var data = CafeManager.Instance.Data;

        if(data.CafeMenuData.Length != _menuPanel.Length)
        {
            Logger.Log("<CafeMenuPanel> data.CafeMenuData.Length != _menuPanel.Length.");
            return;
        }

        for (int i = 0; i < _menuPanel.Length; i++)
        {
            _menuPanel[i].InitializeView(data.CafeMenuData[i]);
        }
    }
}
