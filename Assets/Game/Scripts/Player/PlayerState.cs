using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerState : MonoBehaviour, ITakeDamage
{
    private const string path_PlayerModel = "Model";
    private Transform playerModelParentTrans;
    private Collider2D playerCollider;
    public CharacterData_SO playerData;
    //数值计算方式为小数模式
    [SerializeField]private int HP = 20;
    [SerializeField]private int maxHP = 20;
    [SerializeField]private int hpRegeneraePerSecond = 0;
    [SerializeField]private float lifeStealRate = 0.0f;
    [SerializeField]private float damageMul = 1.0f;
    [SerializeField]private float attackSpeedMul = 1.0f;
    [SerializeField]private float criticalRate = 0.0f;
    [SerializeField]private int attackRange = 450;
    [SerializeField]private float moveSpeed = 10;
    [SerializeField]private int exp = 0;
    [SerializeField]private int bonusCoin = 0;
    [SerializeField]private int currentPlayerLevel = 1;
    [SerializeField]private bool invincible = false;

    private void Awake() {
        playerModelParentTrans = transform.Find(path_PlayerModel);
        playerCollider = GetComponent<Collider2D>();
    }

    private void Start() {
        AutoRegenerateHealth();
    }

    private void OnEnable() {
        EventManager.instance.onSetCharacterData += SetCharacterData;
        EventManager.instance.onInitPlayerStatus += InitData;
        EventManager.instance.onCollectExpBall += CollectExpBall;
        EventManager.instance.onChangeBonusCoinCount += ChangeBonusCoinCount;
        EventManager.instance.onUpdatePlayerCurrentHP += UpdatePlayerCurrentHP;
        EventManager.instance.onSetPlayerInvincible += SetPlayerInvincible;
    }

    private void OnDisable() {
        EventManager.instance.onSetCharacterData -= SetCharacterData;
        EventManager.instance.onInitPlayerStatus -= InitData;
        EventManager.instance.onCollectExpBall -= CollectExpBall;
        EventManager.instance.onChangeBonusCoinCount -= ChangeBonusCoinCount;
        EventManager.instance.onUpdatePlayerCurrentHP -= UpdatePlayerCurrentHP;
        EventManager.instance.onSetPlayerInvincible -= SetPlayerInvincible;
    }

    private void SetPlayerInvincible(bool isInvincible)
    {
        invincible = isInvincible;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag)
        {
            case GameTag.DropItem:
                ExpBall expBall = other.GetComponent<ExpBall>();
                expBall.Collect(transform);
                CollectExpBall(expBall.isBonus);
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
        exp = 0;
        bonusCoin = 0;
        GameCoreData.PlayerProperties.exp = exp;
        GameCoreData.PlayerProperties.bonusCoin = bonusCoin;
        GameCoreData.PlayerProperties.maxHP = maxHP;
        GameCoreData.PlayerProperties.hpRegeneration = hpRegeneraePerSecond;
        GameCoreData.PlayerProperties.lifeSteal = EyreUtility.Round(lifeStealRate * 100);
        GameCoreData.PlayerProperties.damageMul = EyreUtility.Round((damageMul - 1) * 100);
        GameCoreData.PlayerProperties.attackSpeedMul = EyreUtility.Round((attackSpeedMul - 1) * 100);
        GameCoreData.PlayerProperties.criticalRate = EyreUtility.Round(criticalRate * 100);
        GameCoreData.PlayerProperties.attackRange = attackRange;
        GameCoreData.PlayerProperties.moveSpeed = EyreUtility.Round((moveSpeed - 10) * 10);
        Instantiate(playerData.characterPrefab,playerModelParentTrans);
        EventManager.instance.OnInitStatusBar(maxHP);
        playerCollider.enabled = true;
    }

    private void CollectExpBall(bool isBonus)
    {
        int currentLevelExpRequire = GetCurrentLevelExpRequire();
        if(isBonus)
        {
            exp += 2;
            GameCoreData.PlayerProperties.coin += 2;
        }
        else
        {
            exp += 1;
            GameCoreData.PlayerProperties.coin += 1;
        }
        GameCoreData.PlayerProperties.exp = exp;
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
        bonusCoin = Mathf.Clamp(bonusCoin,0,int.MaxValue);
        GameCoreData.PlayerProperties.bonusCoin = bonusCoin;
        EventManager.instance.OnUpdateBonusCoinCount(bonusCoin);
    }

    private int GetCurrentLevelExpRequire()
    {
        return (LevelManager.currentLevel + 3) * (LevelManager.currentLevel + 3);
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
        this.attackRange = attackRange;
        this.moveSpeed = 10 * (1 + (moveSpeed * 0.01f));
        EventManager.instance.OnUpdateHealthBar(HP,this.maxHP);
    }

    public bool TakeDamage(int damage,bool isCritical = false)
    {
        if(HP < 0 || invincible) return false;
        HP = Mathf.Clamp(HP - damage,0,int.MaxValue);
        EventManager.instance.OnUpdateHealthBar(HP,maxHP);
        EventManager.instance.OnHealthBarFlash(Color.red,Color.white);
        //死亡
        if(HP == 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gameoverSFX);
            LevelManager.levelStatus = eLevelStatus.Ended;
            GlobalVar.gameStatus = eGameStatus.Ended;
            playerCollider.enabled = false;
            EventManager.instance.OnClearAllExpBall();
            EventManager.instance.OnLevelEnd();
            EventManager.instance.OnOpenUI(eUIID.FinishMenu);
            EventManager.instance.OnGameover();
            return true;
        }
        return false;
    }

    public void AutoRegenerateHealth()
    {
        EyreUtility.SetDelay(10f / hpRegeneraePerSecond,(()=>
        {
            if(hpRegeneraePerSecond > 0)
            {
                Mathf.Clamp(HP + 1,0,maxHP);
            }
        })).SetLoops(-1);
    }
}
