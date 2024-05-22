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

    public void UpdatePlayerProperties(PlayerProperty playerProperty)
    {
        switch(playerProperty)
        {
            case PlayerProperty.MaxHP:
            break;
            case PlayerProperty.HPRegeneration:
            break;
            case PlayerProperty.StealHP:
            break;
            case PlayerProperty.DamageMul:
            break;
            case PlayerProperty.AttackSpeed:
            break;
            case PlayerProperty.CriticalRate:
            break;
            case PlayerProperty.AttackRange:
            break;
            case PlayerProperty.MoveSpeed:
            break;
        }
        playerState.UpdatePlayerStatus(maxHP,hpRegeneration,stealHP,damageMul,attackSpeed,criticalRate,attackRange,moveSpeed);
    }

}
