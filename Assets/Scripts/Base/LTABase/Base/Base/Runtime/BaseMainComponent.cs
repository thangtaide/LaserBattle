using UnityEngine;
namespace LTA.Base
{
    public class BaseMainComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        T main;

        protected T Main
        {
            get
            {
                if (main == null)
                    main = GetComponent<T>();
                return main;
            }
        }
    }
}
