using UnityEngine;
using TD.Stats;

namespace TD.Interfaces
{
    public interface IEnemy
    {
        public GameObject GetGameObject();
        public Transform GetTransform();
        public EnemyStats GetStats();
    }
}
