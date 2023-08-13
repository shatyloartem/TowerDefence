using TD.Enemies;
using UnityEngine;

namespace TD.Factories
{
    public interface IEnemyFactory
    {
        public Enemy SpawnEnemy(int enemyIndex);
    }
}
