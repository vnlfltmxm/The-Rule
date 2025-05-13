using Script.Prop;
using UnityEngine;

namespace DeathCafe
{
    public class Cafe : MonoBehaviour, IInteractable
    {
        [Header("UIController"), SerializeField] private UIController _controller;
        [Header("Camera"), SerializeField] private GameObject _camera;
        [Header("MoveTransform"), SerializeField] private Transform _moveTransform;
        [Header("OverlapRadius"), SerializeField] private float _radius;

        public GameObject Camera => _camera;
        public bool IsInteractable { get; set; } = true;

        public void Interact()
        {
            TestCode();

            //var playerUtilityController = FindPlayer();

            //if (playerUtilityController == null)
            //    return;

            //CafeManager.Instance.PlayerPrefab = playerUtilityController.gameObject;
            //_controller.EnableCafeUI(playerUtilityController, this);
        }

        public void InitializeTransform(GameObject playerObject)
        {
            playerObject.transform.position = _moveTransform.position;
            _camera.SetActive(true);
        }

        private PlayerUtilityController FindPlayer()
        {
            Collider[] result = new Collider[1];

            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, result, LayerMask.GetMask("Player"));

            if(hitCount == 0)
                return null;

            var playerUtilityController = result[0].GetComponent<PlayerUtilityController>();
            return playerUtilityController;
        }

        #region Test
        private void TestCode()
        {
            Collider[] result = new Collider[1];

            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, result, LayerMask.GetMask("Player"));

            if (hitCount == 0)
                return;

            var testPlayerInputSystem = result[0].GetComponent<TestPlayerInputSystem>();

            CafeManager.Instance.PlayerPrefab = testPlayerInputSystem.gameObject;
            _controller.EnableCafeUI(testPlayerInputSystem, this);
        }
        #endregion
    }
}

