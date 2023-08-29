using TD.Stats;

namespace TD.Interfaces
{
    public interface IUpgradePanelController
    {
        public void SetButtons(UpgradePanelButtonData[] buttons);

        public void OnButtonPressed(UpgradePanelButtonData buttonData);
    }
}
