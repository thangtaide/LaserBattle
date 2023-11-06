using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFPS : MonoBehaviour
{
    TextMeshProUGUI txtFPS;

    private void Awake()
    {
        txtFPS = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetFPS",0,0.2f);
    }

    public float deltaTime;
    public int avgFrameRate;
    // Update is called once per frame
    void GetFPS()
    {
        float current = (1f / Time.unscaledDeltaTime); ;
        avgFrameRate = (int)current;
        if (avgFrameRate >= 30) txtFPS.color = Color.green;
        else if (avgFrameRate >= 20) txtFPS.color = Color.yellow;
        else txtFPS.color = Color.red;
        txtFPS.text = avgFrameRate.ToString() + " FPS";
    }
}
