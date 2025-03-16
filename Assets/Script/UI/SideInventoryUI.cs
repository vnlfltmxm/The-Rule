using System.Collections.Generic;
using UnityEngine;

public class SideInventoryUI : UIBase
{
    protected override void OnAwake()
    {
        base.OnAwake();
        RefreshItemUI();
    }

    private void RefreshItemUI()
    {
        //InventoryManager.Instance.Items
    }
    
    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryDataChangeEvent += RefreshItemUI;
    }
    private void OnDisable()
    {
        InventoryManager.Instance.OnInventoryDataChangeEvent -= RefreshItemUI;
    }
}
