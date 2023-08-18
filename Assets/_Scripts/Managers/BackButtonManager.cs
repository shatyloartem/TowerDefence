using System;
using TD.Singleton;
using UnityEngine;

namespace TD.Managers
{
    public class BackButtonManager : Singleton<BackButtonManager>
    {
        public static event Action OnBackButtonPressed;

        public void BackButton()
        {
            OnBackButtonPressed?.Invoke();
            Debug.Log("Pressed back button");
        }
    }
}
