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

    public void UpdatePlayerProperties(PlayerProperty playerProperty,int changeAmount)
    {
        switch(playerProperty)
        {
            case PlayerProperty.MaxHP:
                maxHP += changeAmount;
            break;
            case PlayerProperty.HPRegeneration:
                hpRegeneration += changeAmount;
            break;
            case PlayerProperty.StealHP:
                stealHP += changeAmount;
            break;
            case PlayerProperty.DamageMul:
                damageMul += changeAmount;
            break;
            case PlayerProperty.AttackSpeed:
                attackSpeed += changeAmount;
            break;
            case PlayerProperty.CriticalRate:
                criticalRate += changeAmount;
            break;
            case PlayerProperty.AttackRange:
                attackRange += changeAmount;
            break;
            case PlayerProperty.MoveSpeed:
                moveSpeed += changeAmount;
            break;
        }
        playerState.UpdatePlayerStatus(maxHP,hpRegeneration,stealHP,damageMul,attackSpeed,criticalRate,attackRange,moveSpeed);
    }

}
