using UnityEngine;
using TD.Singleton;
using TD.Interfaces;

namespace TD.Factories
{
    public class EnemyFactory : Singleton<EnemyFactory>, IEnemyFactory
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _spawnParent;

        [Space(7)]

        [SerializeField] private GameObject[] _enemiesPrefabs;

        public IEnemy SpawnEnemy(int enemyIndex)
        {
            // Spawning new enemy on spawn tile
            IEnemy enemy = Instantiate(_enemiesPrefabs[enemyIndex], _spawnPoint.position, Quaternion.identity).GetComponent<IEnemy>();
            // Setting enemies parent to correctly show in hierarchy
            enemy.GetTransform().parent = _spawnParent;
             
            return enemy;
        }
    }
}
