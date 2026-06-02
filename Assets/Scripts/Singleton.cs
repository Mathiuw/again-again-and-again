using UnityEngine;

namespace MaiNull
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get 
            {
                if (_instance) return _instance;
                _instance = FindFirstObjectByType<T>();
                
                if (_instance) return _instance;
                GameObject singletonGo = new GameObject { name = typeof(T).ToString() };
                _instance = singletonGo.AddComponent<T>();
                return _instance;
            } 
        }

        public virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = GetComponent<T>();

            DontDestroyOnLoad(gameObject);

            if (_instance != null)
                return;
        }
    }
}
