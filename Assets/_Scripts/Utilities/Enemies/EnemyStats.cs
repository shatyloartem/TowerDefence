using UnityEngine;

namespace TD.Enemies
{
    public class EnemyStats : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private int _hp = 100;
        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        [SerializeField]
        [Range(0, 6)]
        private float _speed = 10;
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public void GiveDamage(int damage) => Hp -= damage;
    }
}
