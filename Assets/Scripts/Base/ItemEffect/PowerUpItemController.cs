using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPowerUp
{
    public int level;
}

public class PowerUpItemController : MonoBehaviour, ItemEffect
{
    public LevelPowerUp info;

    public object Info { set => info = (LevelPowerUp)value; }

    public void Active()
    {
        for(int i = 0; i < info.level; i++)
        {
            Player.Instance.OnPowerUp();

        }
        SoundController.instance.PlaySound("power_up_sound");
        Destroy(this);

    }
}
