using LTA.VO;

public class CharactersVO : BaseMutilVO
{
    public CharactersVO()
    {
        LoadData<BaseVO>("Entities", "characterInfo");
    }
}
