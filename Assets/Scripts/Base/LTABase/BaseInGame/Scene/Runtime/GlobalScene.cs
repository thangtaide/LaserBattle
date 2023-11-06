using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScene
{
    static ISceneManager sceneManager;

    public static ISceneManager SceneManager
    {
        get
        {
            if (sceneManager == null)
            {
                Debug.LogError("SceneManager is null");
            }
            return sceneManager;
        }
        set
        {
            sceneManager = value;
        }
    }
}
