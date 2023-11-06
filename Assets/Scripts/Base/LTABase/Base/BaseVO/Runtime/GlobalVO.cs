using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVO
{
    static IGetDataText getDataText;

    static IGetDataTexts getDataTexts;

    static IGetAsset getAsset;
    public static IGetDataText GetDataText
    {
        get
        {
            if (getDataText == null)
            {
                throw new System.Exception("GetDataText null");
            }
            return getDataText;
        }
        set
        {
            getDataText = value;
        }
    }

    public static IGetDataTexts GetDataTexts
    {
        get
        {
            if (getDataTexts == null)
            {
                throw new System.Exception("GetDataTexts null");
            }
            return getDataTexts;
        }
        set
        {
            getDataTexts = value;
        }
    }
    public static IGetAsset GetAsset
    {
        get
        {
            if (getAsset == null)
            {
                throw new System.Exception("GetAsset null");
            }
            return getAsset;
        }
        set
        {
            getAsset = value;
        }
    }
}
