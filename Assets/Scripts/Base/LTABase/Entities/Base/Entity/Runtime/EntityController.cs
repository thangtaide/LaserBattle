
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Entity : IEqualityComparer<Entity>
{
    public static IEqualityComparer<Entity> EntityComparer = new Entity("",0);
    public string name;
    public int level;
    public Entity(string name, int level)
    {
        this.name = name;
        this.level = level;
    }

    public bool Equals(Entity entity1, Entity entity2)
    {
        if (entity2 is null) return entity1 is null;
        return entity1.name == entity2.name && entity1.level == entity2.level;
    }
    public int GetHashCode(Entity obj)
    {
        return 1;
    }

    public static bool operator ==(Entity entity1, Entity entity2)
    {
        if (entity2 is null) return entity1 is null;
        return entity1.name == entity2.name && entity1.level == entity2.level;
    }

    public static bool operator !=(Entity entity1, Entity entity2)
    {
        if (entity2 is null) return !(entity1 is null);
        return entity1.name != entity2.name || entity1.level != entity2.level;
    }
}
public class EntityController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
