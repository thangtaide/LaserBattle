using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StackEntityController : MonoBehaviour
{
    Stack<Entity> entities = new Stack<Entity>();

    public int Count
    {
        get
        {
            return entities.Count;
        }
    }

    public void PickEntity(Entity entity)
    {
        if (entity == null) return;
        entities.Push(entity);
        IOnPickEntity[] onPickEntities = GetComponentsInChildren<IOnPickEntity>();
        foreach (IOnPickEntity onPickEntity in onPickEntities)
        {
            onPickEntity.OnPickEntity(entity, this);
        }
    }
    public GameObject ThrowEntity()
    {
        Entity entity = RemoveEntity();
        if (entity == null) return null;
        GameObject entityOnject = Entities.Instance.CreateEntity(entity, transform.position);
        IOnThrowEntity[] onThrowEntities = GetComponentsInChildren<IOnThrowEntity>();
        foreach (IOnThrowEntity onThrowEntity in onThrowEntities)
        {
            onThrowEntity.OnThrowEntity(entity, this, entityOnject);
        }
        return entityOnject;
    }

    public Entity RemoveEntity()
    {
        if (entities.Count == 0) return null;
        Entity entity = entities.Pop();
        IOnRemoveEntity[] onRemoveEntities = GetComponentsInChildren<IOnRemoveEntity>();
        foreach (IOnRemoveEntity onRemoveEntity in onRemoveEntities)
        {
            onRemoveEntity.OnRemoveEntity(entity, this);
        }
        return entity;
    }
}
