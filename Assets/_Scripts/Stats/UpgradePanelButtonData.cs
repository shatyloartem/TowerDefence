using UnityEngine;

namespace TD.Stats
{
    public class UpgradePanelButtonData
    {
        public Sprite icon;

        public int cost = 100;
        public int turretIndex;

        public bool isThisUpgrade = false;

        public UpgradePanelButtonData(Sprite icon, int cost = default, int turretIndex = default, bool isThisUpgrade = default)
        {
            this.icon = icon;
            this.cost = cost;
            this.turretIndex = turretIndex;
            this.isThisUpgrade = isThisUpgrade;
        }
    }
}
