using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRocketSkillController : MonoBehaviour
{
    [SerializeField] float timeActive = 3f;
    [SerializeField] float activeEachTime = 0.25f;
    float timeNextSkill;
    float timeEndActive;
    bool isActive;
    private void Start()
    {
        isActive = false;
    }
    public void ActiveSkill()
    {
        if (!isActive)
        {
            isActive = true;
            timeEndActive = Time.time + timeActive;
            timeNextSkill = Time.time;
            OnActive();
        }
    }
     void OnActive()
    {
        RocketController clone1 = Create.Instance.CreateRocket(transform, true);
        RocketController clone2 = Create.Instance.CreateRocket(transform,false);
        clone1.DmgBullet = Player.Instance.currentDmg * 3;
        clone2.DmgBullet = Player.Instance.currentDmg * 3;
        SoundController.instance.PlaySound("Rocket");
    }
    private void Update()
    {
        if (!isActive) { return; }
        if (Time.time < timeEndActive)
        {
            if (Time.time - timeNextSkill > activeEachTime)
            {
                timeNextSkill = Time.time + activeEachTime;
                OnActive();
            }
        }
        else isActive = false;
    }
}
