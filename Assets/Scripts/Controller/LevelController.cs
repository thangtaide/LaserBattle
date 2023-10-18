using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.DesignPattern;
using System;
using TMPro;

public class LevelController : ProcessingController
{
    [SerializeField] TextMeshProUGUI lvlText;
    [SerializeField] float expMultiplier = 1.2f;
    float expLvl1;
    void Awake()
    {
        expLvl1 = maxValue;
    }
    int level = 1;
    public int Level {
        get
        {
            return level;
        }
        set {
            this.level = value;
            if(lvlText != null) 
            {
                lvlText.text = "Lv." + this.level.ToString();
            }
            if (OnLevelUp != null)
            {
                OnLevelUp(level);
                maxValue = CalculateExpForNextLv(level);

            }
        } }
    public Action<int> OnLevelUp;

    protected override void OnChangeValue(float value)
    {
        while (currentValue >= maxValue)
        {
            currentValue -= maxValue;
            Level++;
        }
    }

    float CalculateExpForNextLv(int lv)
    {
        if (lv <= 1) { return expLvl1; }
        else
        {
            return CalculateExpForNextLv(lv - 1) * expMultiplier;
        }
    }
}
