using UnityEngine;

namespace TD.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        private void OnEnable()
        {
            if (Instance == null)
                Instance = this as T;
        }

        private void OnDestroy()
        {
            if(Instance == this)
                Instance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

/*    public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            base.Awake();
        }
    }*/
}
