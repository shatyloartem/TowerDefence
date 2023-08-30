using TD.Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using TD.Interfaces;

namespace TD.UI
{
    public class UpgradeButtonController : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI costText;

        [Inject]
        private IUpgradePanelController panelController;

        private UpgradePanelButtonData data;

        public void UpdateButton(UpgradePanelButtonData data)
        {
            if(icon != null)
                icon.sprite = data.icon;
    
            if(costText != null)
                costText.text = data.cost.ToString();

            this.data = data;
        }

        public void PressButton()
        {
            UpgradePanelController.Instance.OnButtonPressed(data);

            // panelController.OnButtonPressed(data);
        }
    }
}
