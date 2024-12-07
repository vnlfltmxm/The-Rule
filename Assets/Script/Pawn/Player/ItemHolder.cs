using Script.Pawn.Player;
using Script.Prop;
using UnityEngine;

public class ItemHolder
{
    private Item _item = null;
    private bool _hasItem = false;
    
    private Transform _handTransform;

    private float _itemThrowForce;

    public void Init(Player player)
    {
        _handTransform = player.HandTransform;
    }
    public void AddItem(Item item)
    {
        if (_hasItem == true)
            DropItem();
        _hasItem = true;
        _item = item;

        item.Collider.enabled = false;
        item.Rigidbody.isKinematic = true;
        
        _item.transform.SetParent(_handTransform);
        _item.transform.position = _handTransform.position;
    }

    public void DropItem()
    {
        _item.Collider.enabled = true;
        _item.Rigidbody.isKinematic = false;

        _item.transform.SetParent(null);
        _item.Rigidbody.AddForce(_handTransform.forward * _itemThrowForce, ForceMode.Impulse);
        
        _hasItem = false;
        _item = null;
    }
}
