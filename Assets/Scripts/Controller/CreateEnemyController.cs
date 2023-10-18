using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateEnemyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messNotice;
    [SerializeField] float timeEachSummon = 2f;
    float timeSummon;
    int waveCount;
    int turnCount;
    FillerTargetController fillerTargetController;
    
    private void Start()
    {
        turnCount = 1;
        waveCount = 1;
        timeSummon = timeEachSummon + Time.time;
        fillerTargetController = GetComponent<FillerTargetController>();
    }
    void Update()
    {
        if (Player.Instance != null)
        {
            Transform target = TargetController.GetTarget(fillerTargetController);
            if (target != null) {
                timeSummon = Time.time + timeEachSummon;
                return; }

            if (turnCount <= 5)
            {
                if (Time.time > timeSummon)
                {
                    SummonWave(turnCount + waveCount);
                    turnCount++;
                }
            } else if (turnCount == 6)
            {
                
                
                    turnCount++; 
                    if (messNotice.gameObject.activeSelf)
                    {
                        messNotice.gameObject.SetActive(false);
                    }
                    messNotice.text = "BOSS";
                    messNotice.gameObject.SetActive(true);
                    StartCoroutine(CreateBoss());
                
            }
            else
            {
                if (Time.time > timeSummon+0.5f)
                {
                    turnCount = 1;
                    waveCount++;
                    timeSummon = Time.time + timeEachSummon;
                    if (messNotice.gameObject.activeSelf)
                    {
                        messNotice.gameObject.SetActive(false);
                    }
                    messNotice.text = "WAVE " + waveCount;
                    messNotice.gameObject.SetActive(true);
                }
            } 
        }
    }
    IEnumerator CreateBoss()
    {
        yield return new WaitForSeconds(timeEachSummon);
        Create.Instance.CreateBoss(waveCount);
    }
    void SummonWave(int enemyCount)
    {
            int level = enemyCount / 2;
            for (int j = 0; j < enemyCount; j++)
            {
                Create.Instance.CreateEnemy(level);
            }
    }
}
