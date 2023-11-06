using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.DesignPattern;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : SpaceController
{
    public static float multiDmg = 1f;
    [SerializeField] float mulDmg = 1f;
    [SerializeField] JoystickController moveJoystick;

    [SerializeField] ShieldController shieldController;
    FireRocketSkillController fireRocketSkill;

    [SerializeField] PanelCDController panelShieldCD;
    [SerializeField] PanelCDController panelRocketCD;
    int currentPower = 1;
    public int currenMoney;
    protected override void Awake()
    {
        multiDmg *= mulDmg;
        base.Awake();
        currenMoney = 0;
        fireRocketSkill = GetComponentInChildren<FireRocketSkillController>();
        ObServer.Instance.AddObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        ObServer.Instance.AddObserver(TOPICNAME.BOSS_DIE, OnBossDie);
    }
    void OnEnemyDie(object data)
    {
        if(lvController== null) return;
        EnemyController enemy = (EnemyController)data;
        lvController.CurrentValue += enemy.lvController.Level * 10;
    }
    void OnBossDie(object data)
    {
        if (lvController == null) return;
        BossController enemy = (BossController)data;
        lvController.CurrentValue += enemy.lvController.Level * 150;
    }
    protected override void OnDie()
    {
        Create.Instance.CreateExplosionSpace(transform);
        ObServer.Instance.RemoveObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        ObServer.Instance.Notify(TOPICNAME.PLAYER_DIE, this);
        ObServer.Instance.RemoveObserver(TOPICNAME.PLAYER_DIE, OnEnemyDie);
        ObServer.Instance.RemoveObserver(TOPICNAME.BOSS_DIE, OnBossDie);

        SoundController.instance.PlaySound("ExplosionEffect");
        SoundController.instance.PlaySound("GameOver");
        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !panelShieldCD.isActiveAndEnabled)
        {
            shieldController.ActiveShield();
            panelShieldCD.OnPress();
        }
        if (Input.GetKeyDown(KeyCode.E) && !panelRocketCD.isActiveAndEnabled)
        {
            fireRocketSkill.ActiveSkill();
            panelRocketCD.OnPress();
        }
        Vector3 playerDirection;
        if (moveJoystick.Direction == Vector2.zero)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            playerDirection = new Vector3(horizontal, vertical, 0);
        }
        else { playerDirection = moveJoystick.Direction; }
        Move(playerDirection);
        Shoot();

    }
    protected override void Shoot()
    {
        if (Time.time - spawnShoot > timeToNextShoot && prefabBullet != null)
        {
            spawnShoot = Time.time;
            for(int i = 0; i < currentPower; i++)
            {
                float positionX = transform.position.x - (float)(currentPower - 2 * i - 1) * 0.06f;
                float positionY = transform.position.y - Mathf.Abs(((float)(currentPower-1)/2 - i) * 0.12f);
                Vector3 position = new Vector3(positionX, positionY, transform.position.z);
                BulletController clone = Create.Instance.CreateBullet(position,transform.up, prefabBullet);
                clone.DmgBullet = currentDmg;
            }
            SoundController.instance.PlaySound("laser_shot");
        }
    }
    public void OnPowerUp()
    {
        if (currentPower >= 7) return;
        currentPower++;
    }
    protected override void OnLevelUp(int level)
    {
        base.OnLevelUp(level);
        if (level != 1)
        {
            SoundController.instance.PlaySound("LevelUp");
        }
        if (lvController.Level > 1)
        {
            ObServer.Instance.Notify(TOPICNAME.LEVEL_UP, this);
        }
    }
    protected override SpaceInfo GetSpaceInfor(int level)
    {
        SpaceInfo spaceInfo = DataManager.Instance.playerVO.GetSpaceInfo(level);
        spaceInfo.damage = (int)(spaceInfo.damage * multiDmg);
        return spaceInfo;
    }

}

public class Player : SingletonMonoBehaviour<PlayerController> { }
