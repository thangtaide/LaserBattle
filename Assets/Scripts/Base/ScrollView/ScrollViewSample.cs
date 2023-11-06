using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSample : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabList;

    [Space(10)]
    [Header("Scroll View Events")]
    [SerializeField] private ChoseButtonEvent _onSelectEvent;
    [SerializeField] private ChoseButtonEvent _onSubmitEvent;
    [SerializeField] private ChoseButtonEvent _onClickEvent;

    [Space(10)]
    [Header("Default Selected Index")]
    [SerializeField] public static int _defaultSelectedIndex = 0;

    [Space(10)]
    [Header("For Testing Only")]
    [SerializeField] private int _testButtonCount = 1;
    private void Awake()
    {
        if(_defaultSelectedIndex>= _content.childCount)
        {
            _defaultSelectedIndex = _content.childCount-1;
        }
    }
    void Start()
    {/*
        if (_testButtonCount > 0)
        {
            TestCreateItem(_testButtonCount);
        }*/
        UpdateAllButtonNavigationReferences();
    }
    private void OnEnable()
    {
        _defaultSelectedIndex = PlayerPrefs.GetInt("Player", 0);
        StartCoroutine(DelaySelectChild(_defaultSelectedIndex));
    }

    public void SelectChild(int index)
    {
        int childCount = _content.childCount;
        if (index >= childCount) { return; }
        ChoseButton item = _content.GetChild(index).GetComponentInChildren<ChoseButton>();
        item.ObtainSelectionFucus();
        PlayerPrefs.SetInt("Player", index);
    }

    IEnumerator DelaySelectChild(int index)
    {
        yield return new WaitForSeconds(0.2f);
        SelectChild(index);
    }


    private void UpdateAllButtonNavigationReferences()
    {
        ChoseButton[] children = _content.transform.GetComponentsInChildren<ChoseButton>();
        if (children.Length < 2)
        {
            return;
        }
        ChoseButton item;
        Navigation navigation;

        for (int i = 0; i < children.Length; i++)
        {
            item = children[i];
            navigation = item.gameObject.GetComponentInChildren<Button>().navigation;
            navigation.selectOnLeft = SelectNavigationOnLeft(i, children.Length);
            navigation.selectOnRight = SelectNavigationOnRight(i, children.Length);
            item.gameObject.GetComponentInChildren<Button>().navigation = navigation;
        }

    }

    private Selectable SelectNavigationOnRight(int indexCurrent, int totalEntries)
    {
        ChoseButton item;
        if (indexCurrent == totalEntries - 1)
        {
            item = _content.transform.GetChild(0).GetComponentInChildren<ChoseButton>();
        }
        else
        {
            item = _content.transform.GetChild(indexCurrent + 1).GetComponentInChildren<ChoseButton>();
        }
        return item.GetComponent<Selectable>();
    }

    private Selectable SelectNavigationOnLeft(int indexCurrent, int totalEntries)
    {
        ChoseButton item;
        if (indexCurrent == 0)
        {
            item = _content.GetChild(totalEntries - 1).GetComponentInChildren<ChoseButton>();
        }
        else
        {
            item = _content.GetChild(indexCurrent - 1).GetComponentInChildren<ChoseButton>();
        }
        return item.GetComponent<Selectable>();
    }


    private void TestCreateItem(int testButtonCount)
    {
        for (int i = 0; i < testButtonCount; i++)
        {
            TestCreate("Player_" + i);
        }
    }

    private ChoseButton TestCreate(string strName)
    {
        GameObject gObj;
        ChoseButton temp;

        gObj = Instantiate(_prefabList, Vector3.zero, Quaternion.identity);
        gObj.transform.SetParent(_content);
        gObj.name = strName;
        gObj.transform.localPosition = Vector3.zero;
        gObj.transform.localScale = Vector3.one;
        gObj.transform.localRotation = Quaternion.Euler(new Vector3());

        temp = gObj.GetComponentInChildren<ChoseButton>();
        temp.ButtonName = strName;

        temp.OnClickEvent.AddListener((ChoseButton) => { HandleOnClick(temp); });
        temp.OnSelectEvent.AddListener((ChoseButton) => { HandleOnSelect(temp); });
        temp.OnSubmitEvent.AddListener((ChoseButton) => { HandleOnSubmit(temp); });

        return temp;
    }

    private void HandleOnSubmit(ChoseButton temp)
    {
        _onSubmitEvent.Invoke(temp);
    }

    private void HandleOnSelect(ChoseButton temp)
    {
        _onSelectEvent.Invoke(temp);
    }

    private void HandleOnClick(ChoseButton temp)
    {
        _onClickEvent.Invoke(temp);
    }

}