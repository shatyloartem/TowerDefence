using Zenject;
using TD.Interfaces;
using TD.UI;
using UnityEngine;

namespace TD.Installers
{
    public sealed class UpgradePanelControllerInstaller : MonoInstaller
    {
        [SerializeField]
        private UpgradePanelController controller;

        public override void InstallBindings()
        {
            Container.
                Bind<IUpgradePanelController>().
                To<UpgradePanelController>().
                FromComponentInNewPrefab(controller).
                AsSingle();
        }
    }
}
