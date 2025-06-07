using System;
using Script.Pawn.Player;
using UnityEngine;

namespace Script.Prop
{
    public class Item : Prop, IInteractable, IUsable
    {
        [HideInInspector] public Collider Collider;
        [HideInInspector] public Rigidbody Rigidbody;

        public bool IsUsable { get; set; } = false;
        public bool IsInteractable { get; set; } = true;
        
        public override void Init(string propId)
        {
            
        }
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }
        public void OnInteract(IInteractor sender)
        {
            Logger.Log("Item Interact");
        }
        public void Use()
        {
            Logger.Log("Item Use");
        }
    }
}