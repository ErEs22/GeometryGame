using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerState : MonoBehaviour, ITakeDamage
{
    private PlayerManager playerManager;
    public PlayerData_SO playerData;
    private int HP = 20;
    private int maxHP = 20;
    private int hpRegeneraePerSecond = 0;
    private float stealHPRate = 0.0f;
    private float damageMul = 1.0f;
    private float attakSpeedMul = 1.0f;
    private float criticalRate = 0.0f;
    private int attackRange = 450;
    private int moveSpeed = 10;
    private int exp = 0;
    private int coin = 0;
    private int currentPlayerLevel = 1;

    private void Awake() {
    }

    private void OnEnable() {
        EventManager.instance.onInitPlayerStatus += InitData;
        EventManager.instance.onCollectExpBall += CollectExpBall;
    }

    private void OnDisable() {
        EventManager.instance.onInitPlayerStatus -= InitData;
        EventManager.instance.onCollectExpBall -= CollectExpBall;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag)
        {
            case GameTag.DropItem:
                other.GetComponent<ExpBall>().Collect(transform);
                CollectExpBall();
            break;
        }
    }

    private void InitData()
    {
        HP = playerData.HP;
        maxHP = playerData.HP;
        hpRegeneraePerSecond = playerData.hpRegeneraePerSecond;
        stealHPRate = playerData.stealHPRate;
        damageMul = playerData.damageMul;
        attakSpeedMul = playerData.attakSpeedMul;
        criticalRate = playerData.criticalRate;
        attackRange = playerData.attackRange;
        moveSpeed = playerData.moveSpeed;
        EventManager.instance.OnInitStatusBar(maxHP);
    }

    private void CollectExpBall()
    {
        int currentLevelExpRequire = GetCurrentLevelExpRequire();
        exp++;
        coin++;
        if(exp >= currentLevelExpRequire)
        {
            //角色升级
            exp = 0;
            currentPlayerLevel++;
            EventManager.instance.OnPlayerUpgradeCountIncrease();
        }
        EventManager.instance.OnUpdateExpBar(currentPlayerLevel,exp,currentLevelExpRequire);
    }

    private int GetCurrentLevelExpRequire()
    {
        return 20 + LevelManager.currentLevel * 5;
    }

    public void UpdatePlayerStatus(int maxHP,int hpRegeneration,int stealHP,int damageMul,int attackSpeed,int criticalRate,int attackRange,int moveSpeed)
    {
        //TODO 游戏中血量未满时最大生命值改变需重新计算当前血量值
        HP = maxHP;
        this.maxHP = maxHP;
        hpRegeneraePerSecond = hpRegeneration;
        stealHPRate = stealHP * 0.01f;
        this.damageMul = 1 + (damageMul * 0.01f);
        attakSpeedMul = 1 + (attackSpeed * 0.01f);
        this.criticalRate = criticalRate * 0.01f;
        this.attackRange = playerData.attackRange + attackRange;
        this.moveSpeed = Mathf.FloorToInt(playerData.moveSpeed * (1 + (moveSpeed * 0.01f)));
        EventManager.instance.OnUpdateHealthBar(HP,this.maxHP);
    }

    public void TakeDamage(int damage)
    {
        if(HP < 0) return;
        HP = Mathf.Clamp(HP - damage,0,int.MaxValue);
        EventManager.instance.OnUpdateHealthBar(HP,maxHP);
        EventManager.instance.OnHealthBarFlash(Color.red,Color.white);
        //TODO击中效果
        if(HP == 0)
        {
            EventManager.instance.OnOpenUI(UIID.FinishMenu);
        }
    }
}
