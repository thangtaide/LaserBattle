using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Base.DesignPattern;
using Unity.Burst.CompilerServices;

public class LvUpText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lvUpTxt;
    [SerializeField] TextMeshProUGUI hpTxt;
    [SerializeField] TextMeshProUGUI damageTxt;
    [SerializeField] TextMeshProUGUI goldTxt;
    [SerializeField] TextMeshProUGUI hPTxt;

    void OnPlayerLevelUp(object data)
    {
        Vector3 position = Player.Instance.GetComponentInParent<Transform>().position+new Vector3 (0.2f,0);
        lvUpTxt.transform.position = position;
        hpTxt.transform.position = position;
        damageTxt.transform.position = position;

        StartCoroutine(TxtActive(lvUpTxt, 0));
        StartCoroutine(TxtActive(hpTxt, 0.5f));
        StartCoroutine(TxtActive(damageTxt, 1f));
    }
    void OnPlayerEarnGold(object data)
    {
        Vector3 position = Player.Instance.GetComponentInParent<Transform>().position + new Vector3(0.2f, 0);
        TextMeshProUGUI gObj = Instantiate(goldTxt, position, Quaternion.identity);
        gObj.transform.SetParent(this.transform, true);
        gObj.gameObject.SetActive(true);
        Destroy(gObj.gameObject, 1.5f);
    }
    void OnPlayerIncreaseHP(object data)
    {
        Vector3 position = Player.Instance.GetComponentInParent<Transform>().position + new Vector3(0.2f, 0);
        TextMeshProUGUI gObj = Instantiate(hpTxt, position, Quaternion.identity);
        gObj.transform.SetParent(this.transform, true);
        gObj.gameObject.SetActive(true);
        Destroy(gObj.gameObject, 1.5f);
    }

    IEnumerator TxtActive(TextMeshProUGUI text, float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(true);
        StartCoroutine(TxtDeactive(text, 1f));
    }
    IEnumerator TxtDeactive(TextMeshProUGUI text, float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
    }

    private void Awake()
    {
        ObServer.Instance.AddObserver(TOPICNAME.LEVEL_UP, OnPlayerLevelUp);
        ObServer.Instance.AddObserver(TOPICNAME.EARNED_GOLD, OnPlayerEarnGold);
        ObServer.Instance.AddObserver(TOPICNAME.INCREASE_HP, OnPlayerIncreaseHP);
    }
}
