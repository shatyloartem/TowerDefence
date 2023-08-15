using Zenject;
using TD.Managers;

namespace TD.Installers
{
    public sealed class TileUpgradePanelManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.
                Bind<ITileUpgradePanelManager>().
                To<TileUpgradePanelManager>().
                FromComponentInNewPrefab(GetComponent<TileUpgradePanelManager>()).
                AsSingle();
        }
    }
}
