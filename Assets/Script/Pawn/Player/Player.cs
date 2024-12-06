using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Pawn.Player
{
    public class Player : Pawn
    {
        public PlayerBodyCondition PlayerBodyCondition;
        public PlayerInputHandler PlayerInputHandler;
        public ItemHolder ItemHolder;
        
        [HideInInspector]public PlayerController PlayerController;
        [HideInInspector] public PlayerInput PlayerInput;
        [HideInInspector] public Rigidbody Rigidbody;
        
        public GameObject VirtualCamera;

        protected override void Init()
        {
            PlayerBodyCondition = new PlayerBodyCondition();
            PlayerInputHandler = new PlayerInputHandler();
            ItemHolder = new ItemHolder();
            
            PlayerController = GetComponent<PlayerController>();
            PlayerInput = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody>();
        
            PlayerBodyCondition.Init();
            PlayerInputHandler.Init(PlayerInput);
            PlayerController.Init(this);
        }
    }

}

