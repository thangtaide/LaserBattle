using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelCDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cdSkill;
    [SerializeField] float timeCD = 10f;
    float CD;
    bool isCD;

    private void Start()
    {
        isCD = false;
        gameObject.SetActive(isCD);
    }
    private void Update()
    {
        if (CD>0)
        {
            if (CD > 1)
            {
                cdSkill.text = Mathf.FloorToInt(CD).ToString();
            }
            else
            {
                cdSkill.text = CD.ToString("F1");
            }
            CD -= Time.deltaTime;
        }
        else
        {
            isCD = false;
            gameObject.SetActive(isCD);
        }
    }
    public void OnPress()
    {
        isCD = true;
        CD = timeCD;
        gameObject.SetActive(isCD);
    }
}
