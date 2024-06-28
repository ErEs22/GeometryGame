using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropertyHandler : MonoBehaviour
{
    private PlayerState playerState;
    public static int maxHP = 100;
    public static int hpRegeneration = 1;
    public static int stealHP = 5;
    public static int damageMul = 0;
    public static int attackSpeed = 0;
    public static int criticalRate = 0;
    public static int attackRange = 0;
    public static int moveSpeed = 0;

    private void Awake() {
        playerState = GetComponent<PlayerState>();
        InitData();
    }

    private void InitData()
    {
        maxHP = playerState.playerData.HP;
    }
    
    private void OnEnable() {
        EventManager.instance.onUpdatePlayerProperty += UpdatePlayerProperties;
    }

    private void OnDisable() {
        EventManager.instance.onUpdatePlayerProperty -= UpdatePlayerProperties;
    }

    public void UpdatePlayerProperties(ePlayerProperty playerProperty,int changeAmount)
    {
        switch(playerProperty)
        {
            case ePlayerProperty.MaxHP:
                maxHP = GameCoreData.PlayerProperties.maxHP;
            break;
            case ePlayerProperty.HPRegeneration:
                hpRegeneration = GameCoreData.PlayerProperties.hpRegeneration;
            break;
            case ePlayerProperty.StealHP:
                stealHP = GameCoreData.PlayerProperties.stealHP;
            break;
            case ePlayerProperty.DamageMul:
                damageMul = GameCoreData.PlayerProperties.damageMul;
            break;
            case ePlayerProperty.AttackSpeed:
                attackSpeed = GameCoreData.PlayerProperties.attackSpeedMul;
            break;
            case ePlayerProperty.CriticalRate:
                criticalRate = GameCoreData.PlayerProperties.criticalRate;
            break;
            case ePlayerProperty.AttackRange:
                attackRange = GameCoreData.PlayerProperties.attackRange;
            break;
            case ePlayerProperty.MoveSpeed:
                moveSpeed = GameCoreData.PlayerProperties.moveSpeed;
            break;
        }
        playerState.UpdatePlayerStatus(maxHP,hpRegeneration,stealHP,damageMul,attackSpeed,criticalRate,attackRange,moveSpeed);
    }

}
