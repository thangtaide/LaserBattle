using LTA.DesignPattern;

public class HPDataController : Singleton<HPDataController>
{
    public HPVO hpVO;
    public void LoadLocalData()
    {
        hpVO = new HPVO();
    }
}
