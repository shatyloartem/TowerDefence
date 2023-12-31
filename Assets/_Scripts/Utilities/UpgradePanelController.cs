using System.Collections.Generic;
using TD.Interfaces;
using TD.Components;
using TD.Singleton;
using TD.Stats;
using UnityEngine;
using UnityEngine.UI;
using TD.Managers;

namespace TD.UI
{
    public class UpgradePanelController : Singleton<UpgradePanelController>, IUpgradePanelController
    {
        [SerializeField]
        private GameObject buttonPrefab;

        [SerializeField]
        private RectTransform buttonsSpawnParent;

        private HorizontalLayoutGroupFixed layoutGroup;

        private RectTransform Transform;

        private List<GameObject> spawnedButtons = new List<GameObject>();

        protected override void Awake()
        {
            base.Awake();

            Transform = GetComponent<RectTransform>();
            layoutGroup = buttonsSpawnParent.GetComponent<HorizontalLayoutGroupFixed>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (Transform == null) Transform = GetComponent<RectTransform>();

            if (layoutGroup == null) buttonsSpawnParent.GetComponent<HorizontalLayoutGroup>();
        }

        public void SetButtons(UpgradePanelButtonData[] buttons)
        {
            // Destroy all previous button
            foreach(GameObject button in spawnedButtons)
                Destroy(button);

            spawnedButtons.Clear();

            // Spawn new buttons
            foreach (UpgradePanelButtonData button in buttons)
            {
                GameObject spawned = Instantiate(buttonPrefab, buttonsSpawnParent);
                
                spawned.GetComponent<UpgradeButtonController>().UpdateButton(button);

                spawnedButtons.Add(spawned);
            }

            layoutGroup.UpdateChildren(spawnedButtons);

            // Fit panel size
            SetPanelSize();
        }

        public void OnButtonPressed(UpgradePanelButtonData buttonData)
        {
            // TODO: Check if have enough money & minus it

            TileUpgradePanelManager upgradeManager = TileUpgradePanelManager.Instance;

            upgradeManager.AddNewTurret(Instantiate(buttonData.turretPrefab, buttonData.spawnTile));

            upgradeManager.SetUpgradePanelActive(false);
        }

        private void SetPanelSize()
        {
            float offsetFromBorder = Transform.rect.width - buttonsSpawnParent.rect.width;
            float spacing = (spawnedButtons.Count - 1) * layoutGroup.spacing;
            float buttonsSize = spawnedButtons.Count * buttonPrefab.GetComponent<RectTransform>().rect.width;

            Transform.sizeDelta = new Vector2(offsetFromBorder + spacing + buttonsSize, Transform.sizeDelta.y);
        }
    }
}
