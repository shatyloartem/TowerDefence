using UnityEngine;

namespace TD.Turrets
{
    [CreateAssetMenu(fileName = "New Turret Stats", menuName = "Turrets/Stats")]
    public class TurretStatsScriptableObject : ScriptableObject
    {
        public int damage = 10;
        public float fireRange = 4f;
        public float fireCooldown = .5f;
    }
}
