using UnityEngine;
using TMPro;
[DisallowMultipleComponent]
public class NonEntityController : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI txtLevel;
    [SerializeField]
    int level;

    IOnUpLevel[] OnUpLevels
    {
        get
        {
            return GetComponentsInChildren<IOnUpLevel>();
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
    }
    
    public void SetLevel(int level)
    {
        this.level = level;
        if (txtLevel != null)
            txtLevel.text = this.level.ToString();
        UpdateLevelInfo();
    }
    public void SetLevel(string level)
    {
        if (txtLevel != null)
            txtLevel.text = level;
        
    }

    public void UpdateLevelInfo()
    {
        IOnUpLevel[] onUpLevels = OnUpLevels;
        if (onUpLevels == null) return;
        foreach (IOnUpLevel onUpLevel in OnUpLevels)
        {
            onUpLevel.OnUpLevel(level);
        }
    }

    public void UpdateLevelInfo(IOnUpLevel exceptOnUpLevel)
    {
        IOnUpLevel[] onUpLevels = OnUpLevels;
        if (onUpLevels == null) return;
        foreach (IOnUpLevel onUpLevel in OnUpLevels)
        {
            if (exceptOnUpLevel == onUpLevel) continue;
            onUpLevel.OnUpLevel(level);
        }
    }

    public void UpLevel()
    {
        SetLevel(level + 1);
    }
}
