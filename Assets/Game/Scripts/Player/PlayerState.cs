using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, ITakeDamage
{
    private const string path_PlayerModel = "Model";
    private Transform playerModelParentTrans;
    public CharacterData_SO playerData;
    private int HP = 20;
    private int maxHP = 20;
    private int hpRegeneraePerSecond = 0;
    private float lifeStealRate = 0.0f;
    private float damageMul = 1.0f;
    private float attackSpeedMul = 1.0f;
    private float criticalRate = 0.0f;
    private int attackRange = 450;
    private float moveSpeed = 10;
    private int exp = 0;
    private int bonusCoin = 0;
    private int currentPlayerLevel = 1;

    private void Awake() {
        playerModelParentTrans = transform.Find(path_PlayerModel);
    }

    private void OnEnable() {
        EventManager.instance.onSetCharacterData += SetCharacterData;
        EventManager.instance.onInitPlayerStatus += InitData;
        EventManager.instance.onCollectExpBall += CollectExpBall;
        EventManager.instance.onChangeBonusCoinCount += ChangeBonusCoinCount;
        EventManager.instance.onUpdatePlayerCurrentHP += UpdatePlayerCurrentHP;
    }

    private void OnDisable() {
        EventManager.instance.onSetCharacterData -= SetCharacterData;
        EventManager.instance.onInitPlayerStatus -= InitData;
        EventManager.instance.onCollectExpBall -= CollectExpBall;
        EventManager.instance.onChangeBonusCoinCount -= ChangeBonusCoinCount;
        EventManager.instance.onUpdatePlayerCurrentHP -= UpdatePlayerCurrentHP;
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

    private void SetCharacterData(CharacterData_SO data)
    {
        playerData = data;
    }

    private void InitData()
    {
        HP = playerData.HP;
        maxHP = playerData.HP;
        hpRegeneraePerSecond = playerData.hpRegeneratePerSecond;
        lifeStealRate = playerData.lifeStealRate;
        damageMul = playerData.damageMul;
        attackSpeedMul = playerData.attackSpeedMul;
        criticalRate = playerData.criticalRate;
        attackRange = playerData.attackRange;
        moveSpeed = playerData.moveSpeed;
        GameCoreData.PlayerProperties.maxHP = maxHP;
        GameCoreData.PlayerProperties.hpRegeneration = hpRegeneraePerSecond;
        GameCoreData.PlayerProperties.lifeSteal = EyreUtility.Round(lifeStealRate * 100);
        GameCoreData.PlayerProperties.damageMul = EyreUtility.Round((damageMul - 1) * 100);
        GameCoreData.PlayerProperties.attackSpeedMul = EyreUtility.Round((attackSpeedMul - 1) * 100);
        GameCoreData.PlayerProperties.criticalRate = EyreUtility.Round(criticalRate * 100);
        GameCoreData.PlayerProperties.attackRange = 0;
        GameCoreData.PlayerProperties.moveSpeed = EyreUtility.Round((moveSpeed - 1) * 100);
        Instantiate(playerData.characterPrefab,playerModelParentTrans);
        EventManager.instance.OnInitStatusBar(maxHP);
    }

    private void CollectExpBall()
    {
        int currentLevelExpRequire = GetCurrentLevelExpRequire();
        exp++;
        GameCoreData.PlayerProperties.exp++;
        GameCoreData.PlayerProperties.coin++;
        if(exp >= currentLevelExpRequire)
        {
            //角色升级
            exp = 0;
            GameCoreData.PlayerProperties.exp = 0;
            currentPlayerLevel++;
            GameCoreData.PlayerProperties.currentPlayerLevel++;
            EventManager.instance.OnPlayerUpgradeCountIncrease();
        }
        EventManager.instance.OnUpdateExpBar(currentPlayerLevel,exp,currentLevelExpRequire);
        EventManager.instance.OnUpdateCoinCount();
    }

    private void ChangeBonusCoinCount(int changeValue)
    {
        bonusCoin += changeValue;
        EventManager.instance.OnUpdateBonusCoinCount(bonusCoin);
    }

    private int GetCurrentLevelExpRequire()
    {
        return 20 + LevelManager.currentLevel * 5;
    }

    public void UpdatePlayerCurrentHP(int hpChange)
    {
        HP = Mathf.Clamp(HP + hpChange,0,maxHP);
        EventManager.instance.OnUpdateHealthBar(HP,maxHP);
    }

    /// <summary>
    /// 更新玩家基础属性数据
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="hpRegeneration"></param>
    /// <param name="lifeSteal"></param>
    /// <param name="damageMul"></param>
    /// <param name="attackSpeed"></param>
    /// <param name="criticalRate"></param>
    /// <param name="attackRange"></param>
    /// <param name="moveSpeed"></param>
    public void UpdatePlayerStatus(int maxHP,int hpRegeneration,int lifeSteal,int damageMul,int attackSpeed,int criticalRate,int attackRange,int moveSpeed)
    {
        int hpChanged = maxHP - this.maxHP;
        if(hpChanged > 0)
        {
            HP += hpChanged;
        }
        else
        {
            HP = Mathf.Clamp(HP,0,maxHP);
        }
        this.maxHP = maxHP;
        hpRegeneraePerSecond = hpRegeneration;
        lifeStealRate = lifeSteal * 0.01f;
        this.damageMul = 1 + (damageMul * 0.01f);
        attackSpeedMul = 1 + (attackSpeed * 0.01f);
        this.criticalRate = criticalRate * 0.01f;
        this.attackRange = playerData.attackRange + attackRange;
        this.moveSpeed = Mathf.FloorToInt(playerData.moveSpeed * (1 + (moveSpeed * 0.01f)));
        EventManager.instance.OnUpdateHealthBar(HP,this.maxHP);
    }

    public bool TakeDamage(int damage,bool isCritical = false)
    {
        if(HP < 0) return false;
        HP = Mathf.Clamp(HP - damage,0,int.MaxValue);
        EventManager.instance.OnUpdateHealthBar(HP,maxHP);
        EventManager.instance.OnHealthBarFlash(Color.red,Color.white);
        //TODO击中效果
        //死亡
        if(HP == 0)
        {
            EventManager.instance.OnClearAllExpBall();
            EventManager.instance.OnLevelEnd();
            LevelManager.levelStatus = eLevelStatus.Ended;
            GlobalVar.gameStatus = eGameStatus.Ended;
            EventManager.instance.OnOpenUI(eUIID.FinishMenu);
            EventManager.instance.OnGameover();
            return true;
        }
        return false;
    }
}
