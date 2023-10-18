using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    static List<TargetController> targets = new List<TargetController>();
   public static Transform GetTarget(FillerTargetController filler)
    {
        Transform bestTarget = null;
        foreach(TargetController target in targets)
        {
            if (!filler.CheckTarget(target.transform)) continue;
            if (bestTarget == null)
            {
                bestTarget = target.transform;
                continue;
            }
            float distanceCurrentTarget = Vector3.Distance(filler.transform.position, bestTarget.transform.position);
            float distanceTarget = Vector3.Distance(filler.transform.position, target.transform.position);
            if (distanceCurrentTarget > distanceTarget) bestTarget = target.transform;
        }
        return bestTarget;
    }
    void Update()
    {
        
    }
    private void Awake()
    {
        if (!targets.Contains(this))
        {
            targets.Add(this);
        }
    }
    private void OnDestroy()
    {
        if(targets.Contains(this))
        targets.Remove(this);
    }
}
