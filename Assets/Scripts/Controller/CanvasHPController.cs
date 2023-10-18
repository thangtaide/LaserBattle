using Base.DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHPController : MonoBehaviour
{
    HpController hPController;
    private void Update()
    {
        if (hPController != null)
        {
            transform.localScale = new Vector3(hPController.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    private void Awake()
    {
        hPController = Player.Instance.GetComponentInChildren<HpController>();
        ObServer.Instance.AddObserver(TOPICNAME.PLAYER_DIE, OnPlayerDie);
    }
    void OnPlayerDie(object data)
    {
        transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
    }
    private void OnDestroy()
    {
        ObServer.Instance.RemoveObserver(TOPICNAME.PLAYER_DIE, OnPlayerDie);
    }
}
