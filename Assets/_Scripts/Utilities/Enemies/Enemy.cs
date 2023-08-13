using UnityEngine;

namespace TD.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }
        public EnemyStats Stats { get; private set; }

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
            Stats = GetComponent<EnemyStats>();
        }
    }
}
