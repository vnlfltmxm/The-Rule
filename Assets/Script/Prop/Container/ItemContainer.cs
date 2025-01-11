using Script.Prop;
using UnityEngine;

public class ItemContainer : InteractiveProp
{
    [field: SerializeField] public InventoryData InventoryData { get; set; } = new InventoryData();
    
    //public bool IsInteractable { get; set; }
    
    public override void Init(string propId)
    {
        
    }
    public override void Interact()
    {
        Logger.Log("ItemContainer Interact");
    }
}
