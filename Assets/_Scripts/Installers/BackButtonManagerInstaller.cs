using Zenject;
using TD.Managers;

namespace TD.Installers
{
    public sealed class BackButtonManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.
                Bind<IBackButtonManager>().
                To<BackButtonManager>().
                FromComponentInNewPrefab(GetComponent<BackButtonManager>()).
                AsSingle();
        }
    }
}
