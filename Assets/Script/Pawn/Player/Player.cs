using Script.Prop;
using Script.Util;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Script.Pawn.Player
{
    public class Player : Pawn
    {
        private static Player _instance;
        public static Player Instance => _instance;


        private PlayerData _playerData;
        private PlayerBodyCondition _playerBodyCondition;
        private PlayerInputHandler _playerInputHandler;
        private PlayerMovementController _playerMovementController;
        private PlayerUtilityController _playerUtilityController;
        private PlayerInput _playerInput;
        private RaycastPropDetector _raycastPropDetector;
        private Rigidbody _rigidbody;
        
        [Header("시네머신 카메라 타겟 위치"), SerializeField] 
        private Transform _virtualCamera;
        [Header("아이템이 위치할 손 위치"), SerializeField] 
        private Transform _handTransform;
        [Header("아이템 인벤토리 위치"), SerializeField] 
        private Transform _inventoryTransform;

        public Transform HeadTransform;
        public Transform BodyTransform;

        #region ReadOnly프로퍼티
        public PlayerData PlayerData => _playerData;
        public PlayerBodyCondition PlayerBodyCondition => _playerBodyCondition;
        public PlayerInputHandler  PlayerInputHandler => _playerInputHandler;
        public PlayerMovementController PlayerMovementController => _playerMovementController;
        public PlayerUtilityController PlayerUtilityController => _playerUtilityController;
        public PlayerInput PlayerInput => _playerInput;
        public RaycastPropDetector RaycastPropDetector => _raycastPropDetector;
        public Rigidbody Rigidbody => _rigidbody;
        
        public Transform VirtualCamera => _virtualCamera;
        public Transform HandTransform => _handTransform;
        public Transform InventoryTransform => _inventoryTransform;
        #endregion

        protected override void Init()
        {
            _instance = this;

            _playerData = new PlayerData();
            _playerBodyCondition = new PlayerBodyCondition();
            _playerInputHandler = new PlayerInputHandler();
            
            _playerMovementController = GetComponent<PlayerMovementController>();
            _playerUtilityController = GetComponent<PlayerUtilityController>();
            _playerInput = GetComponent<PlayerInput>();
            _raycastPropDetector = GetComponent<RaycastPropDetector>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _playerBodyCondition.Init();
            _playerInputHandler.Init(PlayerInput);
            _playerMovementController.Init(this);
            _playerUtilityController.Init(this);
            _raycastPropDetector.Init(this);
            
            TestInitPlayerData();
            
            UIManager.Instance.OpenUI<InventoryOverlayUI>().Init();
        }

        //TODO: 테스트용 초기화
        private void TestInitPlayerData()
        {
            _playerData.AddItem(0);
            _playerData.AddItem(0);
            _playerData.AddItem(0);
        }
    }

}

