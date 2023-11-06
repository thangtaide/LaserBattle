using UnityEngine;
namespace LTA.Base
{
    [System.Serializable]
    public class TimeAffectInfo
    {
        public float timeEffect = 0;
    }

    public class TimeAffectController<T> : MonoBehaviour where T : TimeAffectInfo
    {
        float timeEffect;

        private float timeAffect;

        bool isActive = false;

        protected bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        public virtual T Info
        {
            set
            {
                timeAffect = value.timeEffect;
                timeEffect = value.timeEffect;
                isActive = true;
            }
        }

        public void ResetTime()
        {
            timeAffect = timeEffect;
        }

        void Update()
        {
            if (!isActive) return;
            if (timeAffect <= 0) return;
            timeAffect -= Time.deltaTime;
            if (timeAffect <= 0)
            {
                Destroy(this);
            }
        }
    }
}
