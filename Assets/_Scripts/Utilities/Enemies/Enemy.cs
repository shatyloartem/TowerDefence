using UnityEngine;
using TD.Interfaces;
using TD.Stats;

namespace TD.Utilities
{
    public class Enemy : MonoBehaviour, IDamagable, IEnemy
    {
        private GameObject GameObject;
        private Transform Transform;
        private EnemyStats Stats;

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
            Stats = GetComponent<EnemyStats>();
        }

        public void GiveDamage(int damage)
        {
            Stats.GiveDamage(damage);
        }

        public GameObject GetGameObject() => GameObject;

        public Transform GetTransform() => Transform;

        public EnemyStats GetStats() => Stats;
    }
}
