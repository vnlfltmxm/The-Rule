using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeathCafe
{
    public class MenuUI : CafeDialogUI
    {
        [Header("Menu"), SerializeField] private GameObject _menuUI;
        [Header("SelectUIObject"), SerializeField] private GameObject _selectUI;

        private Action _unLock;
        private Cafe _cafe;
        
        public void EnableMenuUI()
        {
            _menuUI.SetActive(true);
        }

        public void InitializeMenuUI(Action unLock, Cafe cafe)
        {
            if(_cafe == null)
                _cafe = cafe;

            Action oneTimeAction = null;

            oneTimeAction = () =>
            {
                unLock.Invoke();
                _unLock -= oneTimeAction;
            };

            _unLock += unLock;
        }

        private void UnLockPlayer()
        {
            _unLock.Invoke();
            _cafe.Camera.SetActive(false);
        }

        #region MenuButton
        public void OnClickBuyButton()
        {
            CafeManager.Instance.ResetCafeMenu();
            _menuUI.SetActive(false);
            StartDialog();
        }

        private void StartDialog()
        {
            var dialogData = DialogData.SelectUI;
            StartCoroutine(CafeDialog(dialogData, EnableSelectedUI));
        }

        public void OnClickCancelButton()
        {
            CafeManager.Instance.ResetCafeMenu();
        }

        public void OnClickExitButton()
        {
            CafeManager.Instance.ResetCafeMenu();
            StartTrace();
        }

        private void StartTrace()
        {
            var player = CafeManager.Instance.PlayerPrefab;
            WorkerManager.Instance.StartTracking(player);

            UnLockPlayer();
            _menuUI.SetActive(false);
        }
        #endregion

        #region SelectedUIButton
        private void EnableSelectedUI()
        {
            bool activeSelf = _selectUI.activeSelf ? false : true;
            _selectUI.SetActive(activeSelf);
        }

        public void OnClickYesButton()
        {
            CafeRule._isConcentrate = true;
            CalculatePrice();
        }

        public void OnClickNoButton()
        {
            CalculatePrice();
        }

        private void CalculatePrice()
        {
            CafeManager.Instance.ProcessPayment();
            EnableSelectedUI();

            UnLockPlayer();
        }
        #endregion
    }
}

