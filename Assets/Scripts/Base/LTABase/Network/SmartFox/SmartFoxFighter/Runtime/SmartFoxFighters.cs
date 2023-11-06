using LTA.SFS.Base;
using Sfs2X.Entities.Data;

public class SmartFoxFighters : SmartFoxEntities
{
    public SmartFoxFighters(string listEntitiesName) : base(listEntitiesName)
    {
    }

    public override void OnResponse(ISFSObject data)
    {
        //ListFighterVar.dic_name_list_fighter.Clear();
        base.OnResponse(data);
    }

    protected override void AddEntity(string entityName, int level)
    {
        //ListFighterUtils.AddFighter(listEntitiesName, new PackFighter(entityName,level, new PackItem[0]));
    }
}