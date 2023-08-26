using TD.Interfaces;
using TD.Singleton;
using TD.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class UpgradePanelController : Singleton<UpgradePanelController>, IUpgradePanelController
    {
        [SerializeField]
        private GameObject buttonPrefab;

        [SerializeField]
        private RectTransform buttonsSpawnParent;

        private HorizontalLayoutGroup layoutGroup;

        private RectTransform Transform;

        protected override void Awake()
        {
            Transform = GetComponent<RectTransform>();
            layoutGroup = buttonsSpawnParent.GetComponent<HorizontalLayoutGroup>();
        }

        public void SetButtons(UpgradePanelButtonData[] buttons)
        {
            // Destroy all previous button
            foreach(Transform child in buttonsSpawnParent)
                Destroy(child.gameObject);

            // Spawn new buttons
            foreach(UpgradePanelButtonData button in buttons)
                Instantiate(buttonPrefab, buttonsSpawnParent).GetComponent<UpgradeButtonController>().UpdateButton(button);

            // Fit panel size
            SetPanelSize();
        }

        public void OnButtonPressed(UpgradePanelButtonData buttonData)
        {

        }

        private void SetPanelSize()
        {
            Debug.Log(Transform);
            Debug.Log(buttonsSpawnParent);

            float offsetFromBorder = Transform.rect.width - buttonsSpawnParent.rect.width;
            float spacing = (buttonsSpawnParent.childCount - 1) * layoutGroup.spacing;
            float buttonsSize = buttonsSpawnParent.childCount * buttonPrefab.GetComponent<RectTransform>().rect.width;

            Transform.sizeDelta = new Vector2(offsetFromBorder + spacing + buttonsSize, Transform.sizeDelta.y);
        }
    }
}
