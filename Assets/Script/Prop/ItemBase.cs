using System;
using UnityEngine;

namespace Script.Prop
{
    public class ItemBase : InteractiveProp, IUsable
    {
        [HideInInspector] public Collider Collider;
        [HideInInspector] public Rigidbody Rigidbody;

        #region 데이터
        [SerializeField] private ItemData _itemData;
        public ItemData ItemData => _itemData;
        public int ID => _itemData.ID;
        public string Name => _itemData.Name;
        public Sprite Sprite => _itemData.Sprite;
        #endregion
        
        
        public bool IsUsable { get; set; } = false;
        
        public override void Init(string propId)
        {
            
        }
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }
        public sealed override void Interact()
        {
            Logger.Log("Item Interact");
        }
        public void Use()
        {
            Logger.Log("Item Use");
        }
    }
}