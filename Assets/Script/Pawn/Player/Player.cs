using System;
using Script.Prop;
using Script.Util;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Script.Pawn.Player
{
    public class Player : Pawn, IInteractor
    {
        public static Player S_Player;

        public PlayerBodyCondition PlayerBodyCondition;
        public PlayerInputHandler PlayerInputHandler;
        public ItemInventory ItemInventory;

        [HideInInspector] public PlayerMovementController PlayerMovementController;
        [HideInInspector] public PlayerUtilityController PlayerUtilityController;
        [HideInInspector] public PlayerInput PlayerInput;
        [HideInInspector] public RaycastInteractableDetector RaycastInteractableDetector;
        [HideInInspector] public Rigidbody Rigidbody;

        [Header("시네머신 카메라 타겟 위치")] public Transform VirtualCamera;
        [Header("아이템이 위치할 손 위치")] public Transform HandTransform;

        public Transform HeadTransform;
        public Transform BodyTransform;

        protected override void Init()
        {
            S_Player = this;
            PlayerBodyCondition = new PlayerBodyCondition();
            PlayerInputHandler = new PlayerInputHandler();
            ItemInventory = new ItemInventory();

            PlayerMovementController = GetComponent<PlayerMovementController>();
            PlayerUtilityController = GetComponent<PlayerUtilityController>();
            PlayerInput = GetComponent<PlayerInput>();
            RaycastInteractableDetector = GetComponent<RaycastInteractableDetector>();
            Rigidbody = GetComponent<Rigidbody>();

            PlayerBodyCondition.Init();
            PlayerInputHandler.Init(PlayerInput);
            ItemInventory.Init(this);
            PlayerMovementController.Init(this);
            PlayerUtilityController.Init(this);
            RaycastInteractableDetector.Init(this);
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            DebugManager.Instance.SetDebugData(DebugType.PlayerCurrentArea, CurrentArea.ToString());
        }

        public override void SetActiveForcedEvent(bool isActive)
        {
            PlayerInputHandler.SetActive(!isActive);
            PlayerUtilityController.gameObject.SetActive(!isActive);
            RaycastInteractableDetector.gameObject.SetActive(!isActive);
        }

        public GameObject GameObject => this.gameObject;

        public void Interact()
        {
            Logger.Log("Interact", Color.red);
            if (RaycastInteractableDetector.HasDetected == true)
            {
                IInteractable interactable = RaycastInteractableDetector.CurrentDetectedInteractable;
                
                interactable.OnInteract(this);
            }
        }
        
        //PlayerInput 컴포넌트 제어
        public void ControlLock(bool isLock)
        {
            PlayerInput.enabled = !isLock;
        }
    }
}

