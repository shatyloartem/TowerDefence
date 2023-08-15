using TD.Interfaces;
using TD.Singleton;
using UnityEngine;
using Zenject;

namespace TD.Managers
{
    public class TileUpgradePanelManager : Singleton<TileUpgradePanelManager>, ITileUpgradePanelManager
    {
        [SerializeField]
        private GameObject _upgradePanel;


        private Camera _camera;

        protected override void Awake()
        {
            _camera = Camera.main;
        }

        public void SpawnPanelOnTile(ITile tile)
        {
            Vector3 screenSpacePosition = _camera.WorldToScreenPoint(tile.GetTransform().position);
            
            _upgradePanel.transform.position = screenSpacePosition;
            _upgradePanel.SetActive(true);
        
            
        }

        private void OnBackButtonPressed()
        {

        }
    }

    public interface ITileUpgradePanelManager
    {
        public void SpawnPanelOnTile(ITile tile);
    }
}
