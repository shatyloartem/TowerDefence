using Zenject;
using TD.Managers;

namespace TD.Installers
{
    public sealed class EnemiesManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.
                Bind<EnemiesManager>().
                To<EnemiesManager>().
                FromComponentInNewPrefab(GetComponent<EnemiesManager>()).
                AsSingle();
        }
    }
}
