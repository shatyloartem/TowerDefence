using UnityEngine;
using TD.Singleton;
using TD.Enemies;

namespace TD.Factories
{
    public class EnemyFactory : Singleton<EnemyFactory>, IEnemyFactory
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _spawnParent;

        [Space(7)]

        [SerializeField] private GameObject[] _enemiesPrefabs;

        public Enemy SpawnEnemy(int enemyIndex)
        {
            // Spawning new enemy on spawn tile
            Enemy enemy = Instantiate(_enemiesPrefabs[enemyIndex], _spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
            // Setting enemies parent to correctly show in hierarchy
            enemy.Transform.parent = _spawnParent;
            
            return enemy;
        }
    }
}
