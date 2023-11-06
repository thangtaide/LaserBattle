using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Base.DesignPattern;
using UnityEngine.SceneManagement;
using LTA.LTAScene;

public class TOPICNAME
{
    public const string ENEMY_DIE = "Enemy_Die";
    public const string PLAYER_DIE = "Player_Die";
    public const string BOSS_DIE = "Boss_Die";
    public const string EARNED_GOLD = "Earn_Gold";
    public const string INCREASE_HP = "Increase_Hp";
    public const string LEVEL_UP = "Level_Up";
}

public class GameController : MonoBehaviour
{
    int currentMoney;
    public bool isPause = false;
    //[SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textScoreDie;
    //[SerializeField] TextMeshProUGUI textGold;
    [SerializeField] TextMeshProUGUI textGoldDie;
    [SerializeField] TextMeshProUGUI textTotalGold;
    [SerializeField] GameObject panelPlayerDie;

    public GameObject pausePanel;
    int score = 0;

    public static int TotalMoney { 
        get => GetInt("Money",7900);
        set {
            SetInt("Money", value);
        } 
    }

    private void Awake()
    {
        DataManager.Instance.LoadData();
        currentMoney = 0;
    }
    private void Start()
    {
        pausePanel.SetActive(false);
        ObServer.Instance.AddObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        ObServer.Instance.AddObserver(TOPICNAME.PLAYER_DIE, OnPlayerDie);
        ObServer.Instance.AddObserver(TOPICNAME.BOSS_DIE, OnBossDie);
        ObServer.Instance.AddObserver(TOPICNAME.EARNED_GOLD, OnChangeMoney);
    }
    void OnChangeMoney(object data)
    {
        if (Player.Instance != null)
        {
            currentMoney = Player.Instance.currenMoney;
            /*if (textGold == null) { Debug.Log("TextGold null"); }
            else
            {
                textGold.text = "Gold: " + currentMoney.ToString();*/
            textGoldDie.text = "Gold: " + currentMoney.ToString();
            //}
        }
    }
    void OnBossDie(object data)
    {
        BossController enemy = (BossController)data;

        Create.Instance.CreateItem(GetRandomPos(enemy.gameObject.transform.position, 1.75f), ITEMNAME.GOLD_ITEM);
        Create.Instance.CreateItem(GetRandomPos(enemy.gameObject.transform.position, 1.75f), ITEMNAME.HP_ITEM);
        Create.Instance.CreateItem(GetRandomPos(enemy.gameObject.transform.position, 1.75f), ITEMNAME.POWER_UP_ITEM);
        Create.Instance.CreateItem(GetRandomPos(enemy.gameObject.transform.position, 1.75f), ITEMNAME.POWER_UP_ITEM);


        score += enemy.lvController.Level * 150;
        //textScore.text = "Score: " + score.ToString();
        textScoreDie.text = "Score: " + score.ToString();
    }
    Vector3 GetRandomPos(Vector3 position, float rangeRandom)
    {
        return position + new Vector3(Random.Range(-rangeRandom, rangeRandom), Random.Range(-rangeRandom, rangeRandom));
    }
    void OnEnemyDie(object data)
    {
        EnemyController enemy = (EnemyController)data;
        float goldRand = Random.Range(0, 10);
        if (goldRand <= 0.5f)
        {
            Create.Instance.CreateItem(enemy.gameObject.transform.position, ITEMNAME.POWER_UP_ITEM);
        }
        else if (goldRand <= 1.5f)
        {
            Create.Instance.CreateItem(enemy.gameObject.transform.position, ITEMNAME.GOLD_ITEM);
        }
        score += enemy.lvController.Level * 10;
        //textScore.text = "Score: "+score.ToString();
        textScoreDie.text = "Score: " + score.ToString();
    }
    void OnPlayerDie(object data)
    {
        TotalMoney += currentMoney;
        textTotalGold.text = "Total Gold: " + TotalMoney.ToString();
        panelPlayerDie.SetActive(true);
    }
    private void OnDestroy()
    {
        ObServer.Instance.RemoveObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        ObServer.Instance.RemoveObserver(TOPICNAME.PLAYER_DIE, OnPlayerDie);
        ObServer.Instance.RemoveObserver(TOPICNAME.BOSS_DIE, OnBossDie);

    }
    public void OnStart()
    {
        Time.timeScale = 1;
        SoundController.instance.PlaySound("ButtonEffect");
        GameLoadingController.screenName = DefautSceneName.GamePlay;
        SceneManager.LoadScene("Loading");
    }
    public void TogglePause()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        isPause = !isPause;
        if (pausePanel)
        {
            Time.timeScale = isPause ? 0 : 1;
            pausePanel.SetActive(isPause);
        }
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SoundController.instance.PlaySound("ButtonEffect");
        GameLoadingController.screenName = DefautSceneName.Menu;
        SceneManager.LoadScene("Loading");
    }
    public void OnExit()
    {
        SoundController.instance.PlaySound("ButtonEffect");
        Application.Quit();
    }
    public void ActiveRocket()
    {
        if (Player.Instance != null)
        {
            FireRocketSkillController fireRocket = Player.Instance.GetComponent<FireRocketSkillController>();
            fireRocket.ActiveSkill();
        }
    }
    public void ActiveShield()
    {
        if (Player.Instance != null)
        {
            ShieldController shield = Player.Instance.GetComponentInChildren<ShieldController>();
            shield.ActiveShield();
        }
    
    }
    public static int GetInt(string str, int defaultInt)
    {
        return PlayerPrefs.GetInt(str, defaultInt);
    }
    public static int GetInt(string str)
    {
        return PlayerPrefs.GetInt(str, 0);
    }
    public static void SetInt(string str, int i)
    {
        PlayerPrefs.SetInt(str, i);
    }
}
