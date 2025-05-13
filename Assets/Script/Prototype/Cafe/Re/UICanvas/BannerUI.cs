using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;

namespace DeathCafe
{
    public class BannerUI : CafeDialogUI
    {
        [Header("RecommendMenu"), SerializeField] private TextMeshProUGUI _recommendText;
        [Header("SoldOut"), SerializeField] private TextMeshProUGUI[] _soldOutText;

        private void OnEnable()
        {
            RegisterBanner(true);
        }

        private void OnDisable()
        {
            RegisterBanner(false);
        }

        private void Start()
        {
            InitializeBannerUI();
        }

        public void StartDialog(MenuUI menuUI)
        {
            var dialogData = DialogData.OnMenuUI;

            StartCoroutine(CafeDialog(dialogData, menuUI.EnableMenuUI));
        }

        private void RegisterBanner(bool register)
        {
            var cafeManager = CafeManager.Instance;

            if(cafeManager != null)
            {
                CafeManager.Instance.RegisterBanner(InitializeBannerUI, register);
            }
        }

        private void InitializeBannerUI()
        {
            InitializeRecommendText();
            InitializeSoldOutText();
        }

        private void InitializeRecommendText()
        {
            _recommendText.text = string.Empty;
            _recommendText.text = CafeManager.Instance.RecommendedMenu;
        }

        private void InitializeSoldOutText()
        {
            var soldOutSet = CafeManager.Instance.SoldOutSet;
            string[] soldOutArray = soldOutSet.ToArray();

            for(int i = 0; i < soldOutArray.Length; i++)
            {
                if (string.IsNullOrEmpty(soldOutArray[i]))
                    continue;

                _soldOutText[i].text = string.Empty;
                _soldOutText[i].text = soldOutArray[i];
            }
        }
    }
}

