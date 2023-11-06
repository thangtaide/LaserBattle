using LTA.Base.Target;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.AutoTarget
{
    public interface IGetTargetsAuto
    {
        int numTarget
        {
            get;
        }
        FilterTargetController Filter
        {
            get;
        }

        bool IsChangeTarget
        {
            get;
        }

        List<Transform> OldTargets
        {
            get;
        }

        

        void GetTarget(List<Transform> targets);
    }
}
