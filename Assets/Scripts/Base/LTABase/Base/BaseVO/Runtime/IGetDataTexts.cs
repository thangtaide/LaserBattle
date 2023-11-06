using System;
using System.Collections;

public class DataTextInfo
{
    public string data;
    public string name;

    public DataTextInfo(string name,string data)
    {
        this.name = name;
        this.data = data;
    }
}

public interface IGetDataTexts
{
    DataTextInfo[] GetDataTexts(string dataName);
}
