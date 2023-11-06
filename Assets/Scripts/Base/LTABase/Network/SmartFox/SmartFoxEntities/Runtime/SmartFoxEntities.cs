using LTA.SFS.Base;
using Sfs2X.Entities.Data;
using UnityEngine;

public abstract class SmartFoxEntities : IOnResponse
{
    public string listEntitiesName = "Entities";

    public SmartFoxEntities(string listEntitiesName)
    {
        this.listEntitiesName = listEntitiesName;
    }

    public virtual void OnResponse(ISFSObject data)
    {
        ISFSArray items = data.GetSFSArray("Entities");
        for(int i = 0;i < items.Count;i++)
        {
            ISFSObject item = items.GetSFSObject(i);
            AddEntity(item.GetUtfString("EntityName"), item.GetInt("level"));
        }
    }

    protected abstract void AddEntity(string entityName,int level);
}
