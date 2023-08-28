using TD.Stats;
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
            if(icon != null)
                icon.sprite = data.icon;
    
            if(costText != null)
                costText.text = data.cost.ToString();

            this.data = data;
        }

        public void PressButton()
        {
            Debug.Log("Button pressed");

            //UpgradePanelController.Instance.OnButtonPressed(data);
        }
    }
}
