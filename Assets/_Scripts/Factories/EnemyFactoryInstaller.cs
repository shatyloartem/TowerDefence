using Zenject;

namespace TD.Factories
{
    public sealed class EnemyFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.
                Bind<IEnemyFactory>().
                To<EnemyFactory>().
                FromComponentInNewPrefab(GetComponent<EnemyFactory>()).
                AsSingle();
        }
    }
}
