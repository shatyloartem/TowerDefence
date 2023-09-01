using TD.Interfaces;
using TD.Singleton;
using TD.Stats;
using TD.Turrets;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TD.Managers
{
    public class TileUpgradePanelManager : Singleton<TileUpgradePanelManager>, ITileUpgradePanelManager
    {
        [SerializeField]
        private GameObject _upgradePanel;

        private Camera _camera;

        private bool _isPanelSpawned;
        private bool _isRaycastActive = true;

        private TurretStatsScriptableObject[] turrets;
        private List<GameObject> spawnedTurrets;

        private IUpgradePanelController _upgradePanelController;

        protected override void Awake()
        {
            base.Awake();

            _camera = Camera.main;

            BackButtonManager.OnBackButtonPressed += BackButtonPressed;

            turrets = LoadTurrets;

            var i = FindObjectsOfType<MonoBehaviour>(true).OfType<IUpgradePanelController>();
            foreach(IUpgradePanelController upgradePanel in i)
                _upgradePanelController = upgradePanel;
        }

        public void SpawnPanelOnTile(ITile tile)
        {
            if (!_isRaycastActive) return;


            Vector3 screenSpacePosition = _camera.WorldToScreenPoint(tile.GetTransform().position);
            
            _upgradePanel.transform.position = screenSpacePosition;
            SetUpgradePanelActive(true);

            _upgradePanelController.SetButtons(GetButtonsDataFromTurrets(tile));
        }

        private void BackButtonPressed()
        {
            if (!_isPanelSpawned) return;

            SetUpgradePanelActive(false);
        }

        private UpgradePanelButtonData[] GetButtonsDataFromTurrets(ITile tile)
        {
            List<UpgradePanelButtonData> buttons = new List<UpgradePanelButtonData>();

            foreach (var turret in turrets)
                buttons.Add(new UpgradePanelButtonData(turret.buttonIcon, turret.turretPrefab, turret.turretCost, turret.turretIndex));

            return buttons.ToArray();
        }

        public void SetUpgradePanelActive(bool isActive)
        {
            DeactivateRaycast(!isActive);
            _isPanelSpawned = isActive;
            _upgradePanel.SetActive(isActive);
        }

        public void AddNewTurret(GameObject turret) => spawnedTurrets.Add(turret);

        public void RemoveTurret(GameObject turret) => spawnedTurrets.Remove(turret);

        public void DeactivateRaycast(bool isActive) => _isRaycastActive = isActive;

        private TurretStatsScriptableObject[] LoadTurrets => Resources.LoadAll<TurretStatsScriptableObject>("TurretsData");
    }

    public interface ITileUpgradePanelManager
    {
        public void SpawnPanelOnTile(ITile tile);

        public void DeactivateRaycast(bool isActive);
    }
}
