using UnityEngine;
namespace LTA.LTAAutoDestroy
{
    [System.Serializable]
    public class AutoDestroyInfo
    {
        public float timeDestroy;
    }

    public class AutoDestroy : MonoBehaviour,IOnUpLevel
    {
        bool isAutoDestroy = false;

        float timeCountDestroy = -1;

        float timeCount = 0;


        private void Update()
        {
            if (!isAutoDestroy) return;
            if (timeCountDestroy == -1) return;
            if (timeCount <= timeCountDestroy)
            {
                timeCount += Time.deltaTime;
                return;
            }
            Destroy(this.gameObject);
        }

        public void OnUpLevel(int level)
        {
            isAutoDestroy = false;
            AutoDestroyInfo destroyInfo = AutoDestroyDataController.Instance.autoDestroyVO.GetData<AutoDestroyInfo>(name, level);
            if (destroyInfo == null)
            {
                return;
            }
            timeCountDestroy = destroyInfo.timeDestroy;
            isAutoDestroy = true;
        }
    }
}
