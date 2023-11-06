using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Base.Target
{
    [System.Serializable]
    public class TargetInfo
    {
        public SubInfo[] bestTargets;
        public SubInfo[] targets;
    }
    public class FilterTargetController : MonoBehaviour
    {
        TargetInfo info;

        protected List<ICheckTarget> targets = new List<ICheckTarget>();
        protected List<ICheckBestTarget> bestTargets = new List<ICheckBestTarget>();
        public List<ICheckTarget> Targets
        {
            get
            {
                return this.targets;
            }
        }

        public List<ICheckBestTarget> BestTargets
        {
            get
            {
                return this.bestTargets;
            }
        }

        public T GetTarget<T>() where T : class, ICheckTarget
        {
            foreach (ICheckTarget checkTarget in targets)
            {
                if (checkTarget is T) return (T)checkTarget;
            }
            return null;
        }

        public bool CheckTarget(Transform target)
        {
            foreach (ICheckTarget checkTarget in targets)
            {
                if (!checkTarget.CheckTarget(target)) return false;
            }
            return true;
        }

        public bool CheckBestTarget(Transform target1, Transform target2)
        {
            foreach (ICheckBestTarget checkTarget in bestTargets)
            {
                if (!checkTarget.CheckTarget(target1, target2)) return false;
            }
            return true;
        }
        public TargetInfo TargetInfo
        {
            set
            {
                Clear();
                info = value;
                if (value.bestTargets != null)
                {
                    UtilsVO.AddSubInfos(gameObject, value.bestTargets, (effectBestTarget) =>
                    {
                        ICheckBestTarget bestTarget = (ICheckBestTarget)effectBestTarget;
                        bestTargets.Add(bestTarget);
                    });
                }

                if (value.targets != null)
                {
                    UtilsVO.AddSubInfos(gameObject, value.targets, (effectTarget) =>
                    {
                        ICheckTarget target = (ICheckTarget)effectTarget;
                        targets.Add(target);
                    });
                }
            }
            get
            {
                return info;
            }
        }

        void Clear()
        {
            foreach (ICheckBestTarget checkBestTarget in bestTargets)
            {
                Destroy((MonoBehaviour)checkBestTarget);
            }
            bestTargets.Clear();

            foreach (ICheckTarget target in targets)
            {
                Destroy((MonoBehaviour)target);
            }
            targets.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}
