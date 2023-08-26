using TD.Stats;
using TD.Singleton;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TD.UI
{
    public class UpgradeButtonController : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI costText;

        private UpgradePanelButtonData data;

        public void UpdateButton(UpgradePanelButtonData data)
        {
            icon.sprite = data.icon;
            costText.text = data.cost.ToString();

            this.data = data;
        }

        public void PressButton()
        {
            UpgradePanelController.Instance.OnButtonPressed(data);
        }
    }
}
