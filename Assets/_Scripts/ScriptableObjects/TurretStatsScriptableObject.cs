using Codice.Client.BaseCommands;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TD.Turrets
{
    [CreateAssetMenu(fileName = "New Turret Stats", menuName = "Turrets/Stats")]
    public class TurretStatsScriptableObject : ScriptableObject
    {
        [Header("Menu")]
        [TabGroup("Menu")]
        public Sprite buttonIcon;
        [TabGroup("Menu")]
        public int turretCost = 100;
        [TabGroup("Menu")]
        public int turretIndex = 0;

        [Header("Basic")]
        [TabGroup("Basic")]
        public int damage = 10;
        [TabGroup("Basic")]
        public float fireRange = 4f;
        [TabGroup("Basic")]
        public float fireCooldown = .5f;


        [Header("Rotation")]
        [TabGroup("Rotation")]
        public float towerRotationSpeed = 25f;
        [TabGroup("Rotation")]
        public float fireAngle = 20;
    }
}
