using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Script.Pawn.Player
{
    public class Player : Pawn
    {
        public PlayerBodyCondition PlayerBodyCondition;
        public PlayerInputHandler PlayerInputHandler;
        public ItemHolder ItemHolder;
        
        [HideInInspector] public PlayerMovementController PlayerMovementController;
        [HideInInspector] public PlayerUtilityController PlayerUtilityController;
        [HideInInspector] public PlayerInput PlayerInput;
        [HideInInspector] public RaycastPropDetector RaycastPropDetector;
        [HideInInspector] public Rigidbody Rigidbody;
        
        [Header("시네머신 카메라 타겟 위치")] public GameObject VirtualCamera;
        [Header("아이템이 위치할 손 위치")] public Transform HandTransform;

        protected override void Init()
        {
            PlayerBodyCondition = new PlayerBodyCondition();
            PlayerInputHandler = new PlayerInputHandler();
            ItemHolder = new ItemHolder();
            
            PlayerMovementController = GetComponent<PlayerMovementController>();
            PlayerUtilityController = GetComponent<PlayerUtilityController>();
            PlayerInput = GetComponent<PlayerInput>();
            RaycastPropDetector = GetComponent<RaycastPropDetector>();
            Rigidbody = GetComponent<Rigidbody>();
            
            PlayerBodyCondition.Init();
            PlayerInputHandler.Init(PlayerInput);
            ItemHolder.Init(this);
            PlayerMovementController.Init(this);
            PlayerUtilityController.Init(this);
            RaycastPropDetector.Init(this);
        }
    }

}

