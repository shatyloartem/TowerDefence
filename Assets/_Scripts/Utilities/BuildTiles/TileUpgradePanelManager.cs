using TD.Tiles;
using UnityEngine;

namespace TD.UI
{
    public class TileUpgradePanelManager : MonoBehaviour, ITileUpgradePanelManager
    {
        [SerializeField]
        private GameObject _upgradePanel;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void SpawnPanelOnTile(BuildTile tile)
        {
            Vector3 screenSpacePosition = _camera.WorldToScreenPoint(tile.Transform.position);
            
            _upgradePanel.transform.position = screenSpacePosition;
            _upgradePanel.SetActive(true);
        }
    }

    public interface ITileUpgradePanelManager
    {
        public void SpawnPanelOnTile(BuildTile tile);
    }
}
