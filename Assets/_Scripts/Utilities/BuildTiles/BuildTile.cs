using TD.UI;
using UnityEngine;
using Zenject;

namespace TD.Tiles
{
    public class BuildTile : MonoBehaviour, IInteractable
    {
        public Transform Transform { get; private set; }

        [Inject]
        private ITileUpgradePanelManager _upgradePanelManager;

        private void Awake()
        {
            Transform = transform;
        }

        public void Interact()
        {
            _upgradePanelManager.SpawnPanelOnTile(this);
        }
    }
}
