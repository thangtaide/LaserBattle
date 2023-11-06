using UnityEngine;
using System.Collections.Generic;
namespace LTA.Base.Target
{
    public class FilterTargetsController : MonoBehaviour
    {
        Dictionary<Entity, FilterTargetController> dic_Type_Filter = new Dictionary<Entity, FilterTargetController>(Entity.EntityComparer);

        Dictionary<Entity, List<MonoBehaviour>> dic_Type_ListOwnFilter = new Dictionary<Entity, List<MonoBehaviour>>(Entity.EntityComparer);

        public FilterTargetController AddFilterTarget(Entity entity,MonoBehaviour ownUseFilter)
        {
            if (!dic_Type_ListOwnFilter.ContainsKey(entity))
            {
                dic_Type_ListOwnFilter.Add(entity, new List<MonoBehaviour>());
            }
            if (!dic_Type_ListOwnFilter[entity].Contains(ownUseFilter)) 
                dic_Type_ListOwnFilter[entity].Add(ownUseFilter);
            if (dic_Type_Filter.ContainsKey(entity))
            {
                return dic_Type_Filter[entity];
            }
            TargetInfo targetInfo = TargetDataController.Instance.baseTargetVO.GetTargetInfo(entity.name,entity.level);
            if (targetInfo == null) return null;
            FilterTargetController filterTargetController = gameObject.AddComponent<FilterTargetController>();
            filterTargetController.TargetInfo = targetInfo;
            dic_Type_Filter.Add(entity, filterTargetController);
            return filterTargetController;
        }
        public void RemoveFilterTarget(Entity entity, MonoBehaviour ownUseFilter)
        {
            if (!dic_Type_ListOwnFilter.ContainsKey(entity)) return;
            dic_Type_ListOwnFilter[entity].Remove(ownUseFilter);
            if (dic_Type_ListOwnFilter[entity].Count > 0) return;
            dic_Type_ListOwnFilter.Remove(entity);
            if (!dic_Type_Filter.ContainsKey(entity))
            {
                return;
            }
            Destroy(dic_Type_Filter[entity]);
            dic_Type_Filter.Remove(entity);
        }
    }
}
