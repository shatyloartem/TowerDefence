using UnityEngine;

namespace TD.Stats
{
    public class UpgradePanelButtonData
    {
        public Sprite icon;
        public GameObject turretPrefab;

        public Transform spawnTile;

        public int cost = 100;
        public int turretIndex;

        public bool isThisUpgrade = false;

        public UpgradePanelButtonData(Sprite icon, GameObject turretPrefab, Transform spawnTile, int cost = default, int turretIndex = default, bool isThisUpgrade = default)
        {
            this.icon = icon;
            this.turretPrefab = turretPrefab;
            this.spawnTile = spawnTile;
            this.cost = cost;
            this.turretIndex = turretIndex;
            this.isThisUpgrade = isThisUpgrade;
        }
    }
}
