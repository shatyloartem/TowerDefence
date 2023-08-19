using Zenject;
using TD.Managers;
using TD.Interfaces;

namespace TD.Installers
{
    public sealed class EnemiesManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.
                Bind<IEnemiesManager>().
                To<EnemiesManager>().
                FromComponentInNewPrefab(GetComponent<EnemiesManager>()).
                AsSingle();
        }
    }
}
