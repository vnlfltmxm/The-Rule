using System;
using UnityEngine;

namespace DeathCafe
{
    public class UIController : MonoBehaviour
    {
        [Header("BackGroundUI"), SerializeField] private BackGroundUI _backUI;
        [Header("BannerUI"), SerializeField] private BannerUI _bannerUI;
        [Header("MenuUI"), SerializeField] private MenuUI _menuUI;
        [Header("DialogData"), SerializeField] private CafeDialog _dialogData;

        private void Start()
        {
            OnStartUIController();
        }

        private void OnStartUIController()
        {
            CafeDialogUI[] cafeUI = new CafeDialogUI[] { _bannerUI, _menuUI };

            foreach (var cafe in cafeUI)
            {
                cafe.DialogData = _dialogData;
            }
        }

        public void EnableCafeUI(PlayerUtilityController playerUtilityController,
            Cafe cafe)
        {
            playerUtilityController.PlayerLock(true);
            _menuUI.InitializeMenuUI(() => playerUtilityController.PlayerLock(false), cafe);

            var playerObject = playerUtilityController.gameObject;
            _backUI.StartFadeEffect(StartCafeDialog, () => cafe.InitializeTransform(playerObject));
        }

        private void StartCafeDialog()
        {
            _bannerUI.StartDialog(_menuUI);
        }

        #region Test
        public void EnableCafeUI(TestPlayerInputSystem playerUtilityController,
            Cafe cafe)
        {
            playerUtilityController.PlayerLock(true);
            _menuUI.InitializeMenuUI(() => playerUtilityController.PlayerLock(false), cafe);

            var playerObject = playerUtilityController.gameObject;
            _backUI.StartFadeEffect(StartCafeDialog, () => cafe.InitializeTransform(playerObject));
        }
        #endregion
    }
}

