using TD.Enemies;

namespace TD.Factories
{
    public interface IEnemyFactory
    {
        public Enemy SpawnEnemy(int enemyIndex);
    }
}
