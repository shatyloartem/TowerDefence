using TD.Interfaces;
using TD.Singleton;
using UnityEngine;

namespace TD.Managers
{
    public class TileUpgradePanelManager : Singleton<TileUpgradePanelManager>, ITileUpgradePanelManager
    {
        [SerializeField]
        private GameObject _upgradePanel;

        private Camera _camera;

        private bool _isPanelSpawned;

        protected override void Awake()
        {
            _camera = Camera.main;

            BackButtonManager.OnBackButtonPressed += BackButtonPressed;
            //_backButtonManager.OnBackButtonPressed += BackButtonPressed;
        }

        public void SpawnPanelOnTile(ITile tile)
        {
            Vector3 screenSpacePosition = _camera.WorldToScreenPoint(tile.GetTransform().position);
            
            _upgradePanel.transform.position = screenSpacePosition;
            _upgradePanel.SetActive(true);

            _isPanelSpawned = true;

            Debug.Log("Spawned panel");
        }

        private void BackButtonPressed()
        {
            Debug.Log("Back button event invoked");

            if (!_isPanelSpawned) return;

            _isPanelSpawned = false;

            _upgradePanel.SetActive(false);
        }
    }

    public interface ITileUpgradePanelManager
    {
        public void SpawnPanelOnTile(ITile tile);
    }
}
