using System;
using TD.Singleton;

namespace TD.Managers
{
    public class BackButtonManager : Singleton<BackButtonManager>, IBackButtonManager
    {
        public event Action OnBackButtonPressed;

        public void BackButton() => OnBackButtonPressed?.Invoke();
    }

    public interface IBackButtonManager
    {
        public event Action OnBackButtonPressed;
    }
}
