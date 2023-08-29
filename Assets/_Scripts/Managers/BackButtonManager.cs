using System;
using TD.Singleton;

namespace TD.Managers
{
    public class BackButtonManager : Singleton<BackButtonManager>
    {
        public static event Action OnBackButtonPressed;

        public void BackButton()
        {
            OnBackButtonPressed?.Invoke();
        }
    }
}
