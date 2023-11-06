using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AutoCenterScrollView : MonoBehaviour
{
    public Color[] colors;
    public GameObject scrollbar, imageContent;
    private float scroll_pos = 0;
    float[] pos;
    private bool runIt = false;
    private float time;
    private Button takeTheBtn;
    int btnNumber;
    float distance;
    private void Awake()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }
    private void Start()
    {
        scroll_pos = pos[ScrollViewSample._defaultSelectedIndex];
        if (scrollbar != null)
        {
            scrollbar.GetComponent<Scrollbar>().value = scroll_pos;
            ScaleScroll(ScrollViewSample._defaultSelectedIndex);
        }
    }
    void Update()
    {
        if (runIt)
        {
            GecisiDuzenle(distance, pos, takeTheBtn);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0;
                runIt = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    AutoCenterScroll(i, 0.1f);
                    break;
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                ScaleScroll(i);
            }
        }
    }

    void AutoCenterScroll(int selectIndex, float speed)
    {
        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[selectIndex], speed);
    }
    void ScaleScroll(int selectIndex)
    {
        transform.GetChild(selectIndex).localScale = Vector2.Lerp(transform.GetChild(selectIndex).localScale, new Vector2(1f, 1f), 0.05f);
        if (imageContent != null)
        {
            imageContent.transform.GetChild(selectIndex).localScale = Vector2.Lerp(imageContent.transform.GetChild(selectIndex).localScale, new Vector2(1.2f, 1.2f), 0.05f);
            imageContent.transform.GetChild(selectIndex).GetComponent<Image>().color = colors[1];
        }
        for (int j = 0; j < pos.Length; j++)
        {
            if (j != selectIndex)
            {
                if (imageContent != null)
                {
                    imageContent.transform.GetChild(j).GetComponent<Image>().color = colors[0];
                    imageContent.transform.GetChild(j).localScale = Vector2.Lerp(imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
                transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
            }
        }
    }
    private void GecisiDuzenle(float distance, float[] pos, Button btn)
    {
        // btnSayi = System.Int32.Parse(btn.transform.name);

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[btnNumber], 1f * Time.deltaTime);

            }
        }

        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            btn.transform.name = ".";
        }

    }
    public void WhichBtnClicked(Button btn)
    {
        btn.transform.name = "clicked";
        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked")
            {
                btnNumber = i;
                takeTheBtn = btn;
                time = 0;
                scroll_pos = (pos[btnNumber]);
                runIt = true;
            }
        }


    }

}