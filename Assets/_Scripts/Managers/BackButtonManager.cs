using System;
using TD.Singleton;
using UnityEngine;

namespace TD.Managers
{
    public class BackButtonManager : Singleton<BackButtonManager>, IBackButtonManager
    {
        public static event Action OnBackButtonPressed1;
        public event Action OnBackButtonPressed;

        public void BackButton()
        {
            OnBackButtonPressed1?.Invoke();
            Debug.Log("Pressed back button");
        }
    }

    public interface IBackButtonManager
    {
        public event Action OnBackButtonPressed;
    }
}
