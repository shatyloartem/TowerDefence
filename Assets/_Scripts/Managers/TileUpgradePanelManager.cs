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

        private TurretStatsScriptableObject[] turrets;

        private IUpgradePanelController _upgradePanelController;

        protected override void Awake()
        {
            _camera = Camera.main;

            BackButtonManager.OnBackButtonPressed += BackButtonPressed;

            turrets = LoadTurrets;

            var i = FindObjectsOfType<MonoBehaviour>(true).OfType<IUpgradePanelController>();
            foreach(IUpgradePanelController upgradePanel in i)
                _upgradePanelController = upgradePanel;
        }

        public void SpawnPanelOnTile(ITile tile)
        {
            Vector3 screenSpacePosition = _camera.WorldToScreenPoint(tile.GetTransform().position);
            
            _upgradePanel.transform.position = screenSpacePosition;
            _upgradePanel.SetActive(true);

            _isPanelSpawned = true;

            _upgradePanelController.SetButtons(GetButtonsDataFromTurrets());
        }

        private void BackButtonPressed()
        {
            if (!_isPanelSpawned) return;

            _isPanelSpawned = false;

            _upgradePanel.SetActive(false);
        }

        private UpgradePanelButtonData[] GetButtonsDataFromTurrets()
        {
            List<UpgradePanelButtonData> buttons = new List<UpgradePanelButtonData>();

            foreach (var turret in turrets)
                buttons.Add(new UpgradePanelButtonData(turret.buttonIcon, turret.turretCost, turret.turretIndex));

            return buttons.ToArray();
        }

        private TurretStatsScriptableObject[] LoadTurrets => Resources.LoadAll<TurretStatsScriptableObject>("TurretsData");
    }

    public interface ITileUpgradePanelManager
    {
        public void SpawnPanelOnTile(ITile tile);
    }
}
