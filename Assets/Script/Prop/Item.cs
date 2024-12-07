using System;
using UnityEngine;

namespace Script.Prop
{
    public class Item : InteractiveProp, IUsable
    {
        public bool IsUsable { get; set; } = false;
        [HideInInspector] public Collider Collider;
        [HideInInspector] public Rigidbody Rigidbody;
        
        public override void Init(string propId)
        {
            
        }
        private void Start()
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