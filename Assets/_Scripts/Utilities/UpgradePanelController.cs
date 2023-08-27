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

        private void OnEnable()
        {
            if (Transform == null) Transform = GetComponent<RectTransform>();

            if (layoutGroup == null) buttonsSpawnParent.GetComponent<HorizontalLayoutGroup>();
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
            float offsetFromBorder = Transform.rect.width - buttonsSpawnParent.rect.width;
            float spacing = (buttonsSpawnParent.childCount - 1) * layoutGroup.spacing;
            float buttonsSize = buttonsSpawnParent.childCount * buttonPrefab.GetComponent<RectTransform>().rect.width;

            Debug.Log("Buttons count: " + buttonsSpawnParent.childCount);
            Debug.Log($"OffsetFromBorder: {offsetFromBorder}; Spacing: {spacing}; ButtonsSize: {buttonsSize}");

            Transform.sizeDelta = new Vector2(offsetFromBorder + spacing + buttonsSize, Transform.sizeDelta.y);
        }
    }
}
