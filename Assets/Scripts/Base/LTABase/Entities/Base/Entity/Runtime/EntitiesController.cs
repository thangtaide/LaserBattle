using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
public class EntitiesController : MonoBehaviour
{
    [SerializeField]
    GameObject entity;
    
    public GameObject CreateEntity(Entity entityInfo,Vector3 pos)
    {
        GameObject newEntity = Instantiate(entity, pos, entity.transform.rotation);
        newEntity.name = entityInfo.name;
        newEntity.GetComponent<NonEntityController>().SetLevel(entityInfo.level);
        return newEntity;
    }
}

public class Entities : SingletonMonoBehaviour<EntitiesController>
{

}
