using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Script.Pawn.Player
{
    public class Player : Pawn
    {
        public PlayerBodyCondition PlayerBodyCondition;
        public PlayerInputHandler PlayerInputHandler;
        public ItemInventory ItemInventory;
        
        [HideInInspector] public PlayerMovementController PlayerMovementController;
        [HideInInspector] public PlayerUtilityController PlayerUtilityController;
        [HideInInspector] public PlayerInput PlayerInput;
        [HideInInspector] public RaycastPropDetector RaycastPropDetector;
        [HideInInspector] public Rigidbody Rigidbody;
        
        [Header("시네머신 카메라 타겟 위치")] public Transform VirtualCamera;
        [Header("아이템이 위치할 손 위치")] public Transform HandTransform;

        public Transform HeadTransform;
        public Transform BodyTransform;

        protected override void Init()
        {
            PlayerBodyCondition = new PlayerBodyCondition();
            PlayerInputHandler = new PlayerInputHandler();
            ItemInventory = new ItemInventory();
            
            PlayerMovementController = GetComponent<PlayerMovementController>();
            PlayerUtilityController = GetComponent<PlayerUtilityController>();
            PlayerInput = GetComponent<PlayerInput>();
            RaycastPropDetector = GetComponent<RaycastPropDetector>();
            Rigidbody = GetComponent<Rigidbody>();
            
            PlayerBodyCondition.Init();
            PlayerInputHandler.Init(PlayerInput);
            ItemInventory.Init(this);
            PlayerMovementController.Init(this);
            PlayerUtilityController.Init(this);
            RaycastPropDetector.Init(this);
        }
    }

}

