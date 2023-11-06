using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class TimeDown : MonoBehaviour
{
    [SerializeField] private TMP_Text txtEndTime;

    private Action endTimeCallback;
    public void StartTimeDown(float distance, Action endTime = null)
    {
        endTimeCallback = endTime;
        StartCoroutine(TimeCountDown(distance,endTime));
    }

    IEnumerator TimeCountDown(float distance,Action endTime)
    {
        distance--;
        DisplayTime(distance);
        yield return new WaitForSeconds(1);
        if(distance > 0)
            StartCoroutine(TimeCountDown(distance,endTime));
        else
        {
            if (endTimeCallback != null)
                endTimeCallback();
        }
    }
    private void DisplayTime(float timeToDisplay)
    {
        int hours =  (int) (timeToDisplay / 3600) % 24;
        int minutes = (int) (timeToDisplay /60) % 60;
        int seconds =  Mathf.FloorToInt(timeToDisplay % 60);

        txtEndTime.text = "Time : " + LTA.Base.Utils.FormatTime(hours, minutes, seconds);

    }
}
