using UnityEngine;

namespace Base.DesignPattern
{
    public class SingletonMonoBehaviour<T> where T: MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();
                }
                return instance;
            }
        }
        void OnDestroy()
        {
            instance = null;
        }
    }
}
