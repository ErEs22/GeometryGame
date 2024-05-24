using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropertyHandler : MonoBehaviour
{
    private PlayerState playerState;
    private int maxHP = 100;
    private int hpRegeneration = 1;
    private int stealHP = 5;
    private int damageMul = 0;
    private int attackSpeed = 0;
    private int criticalRate = 0;
    private int attackRange = 0;
    private int moveSpeed = 0;

    private void Awake() {
        playerState = GetComponent<PlayerState>();
        InitData();
    }

    private void InitData()
    {
        maxHP = playerState.playerData.HP;
    }

    public void UpdatePlayerProperties(PlayerProperty playerProperty,int changeValue)
    {
        switch(playerProperty)
        {
            case PlayerProperty.MaxHP:
                maxHP += changeValue;
            break;
            case PlayerProperty.HPRegeneration:
                hpRegeneration += changeValue;
            break;
            case PlayerProperty.StealHP:
                stealHP += changeValue;
            break;
            case PlayerProperty.DamageMul:
                damageMul += changeValue;
            break;
            case PlayerProperty.AttackSpeed:
                attackSpeed += changeValue;
            break;
            case PlayerProperty.CriticalRate:
                criticalRate += changeValue;
            break;
            case PlayerProperty.AttackRange:
                attackRange += changeValue;
            break;
            case PlayerProperty.MoveSpeed:
                moveSpeed += changeValue;
            break;
        }
        playerState.UpdatePlayerStatus(maxHP,hpRegeneration,stealHP,damageMul,attackSpeed,criticalRate,attackRange,moveSpeed);
    }

}
