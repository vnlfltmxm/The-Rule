using Script.Util;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Script.Pawn.Player
{
    public class Player : Pawn
    {
        public static Player S_Player;

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
            S_Player = this;
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

        public AreaType GetCurrentArea(Vector3 position)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas))
            {
                int areaIndex = hit.mask; // 현재 위치한 NavMesh Area Index

                foreach (AreaType area in System.Enum.GetValues(typeof(AreaType)))
                {
                    if (NavMesh.GetAreaFromName(area.ToString()) == areaIndex)
                    {
                        return area;
                    }
                }
            }
            return AreaType.Null; // 해당하는 Area 없음
        }
    }
}

