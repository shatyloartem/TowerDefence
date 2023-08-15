using UnityEngine;
using Zenject;
using TD.Managers;
using TD.Interfaces;

namespace TD.Tiles
{
    public class BuildTile : MonoBehaviour, IInteractable, ITile
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

        public Transform GetTransform() => Transform;
    }
}
