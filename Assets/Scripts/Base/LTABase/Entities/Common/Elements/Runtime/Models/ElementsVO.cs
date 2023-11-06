using LTA.VO;
using System;

public class ElementsVO : BaseMutilVO
{
    public ElementsVO()
    {
        LoadData<BaseVO>("Entities", "elementInfo");
    }
}
