using Zenject;
using TD.Factories;
using TD.Interfaces;

namespace TD.Installers
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
