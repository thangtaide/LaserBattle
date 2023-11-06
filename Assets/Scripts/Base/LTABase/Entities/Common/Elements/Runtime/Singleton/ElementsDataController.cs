using LTA.DesignPattern;

public class ElementsDataController : Singleton<ElementsDataController>
{
    public ElementsVO elementsVO;
    public void LoadLocalData()
    {
        elementsVO = new ElementsVO();
    }
}
