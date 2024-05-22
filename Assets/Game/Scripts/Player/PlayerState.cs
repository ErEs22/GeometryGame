using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour, ITakeDamage
{
    public PlayerData_SO playerData;
    private int HP = 20;
    private int hpRegeneraePerSecond = 0;
    private float stealHPRate = 0.0f;
    private float damageMul = 1.0f;
    private float attakSpeedMul = 1.0f;
    private float criticalRate = 0.0f;
    private int attackRange = 450;
    private int moveSpeed = 10;

    private void Awake() {
        InitData();
    }

    private void InitData()
    {
        HP = playerData.HP;
        hpRegeneraePerSecond = playerData.hpRegeneraePerSecond;
        stealHPRate = playerData.stealHPRate;
        damageMul = playerData.damageMul;
        attakSpeedMul = playerData.attakSpeedMul;
        criticalRate = playerData.criticalRate;
        attackRange = playerData.attackRange;
        moveSpeed = playerData.moveSpeed;
    }

    public void UpdatePlayerStatus(int maxHP,int hpRegeneration,int stealHP,int damageMul,int attackSpeed,int criticalRate,int attackRange,int moveSpeed)
    {
        HP = maxHP;
        hpRegeneraePerSecond = hpRegeneration;
        stealHPRate = stealHP * 0.01f;
        this.damageMul = 1 + (damageMul * 0.01f);
        attakSpeedMul = 1 + (attackSpeed * 0.01f);
        this.criticalRate = criticalRate * 0.01f;
        this.attackRange = playerData.attackRange + attackRange;
        this.moveSpeed = Mathf.FloorToInt(playerData.moveSpeed * (1 + (moveSpeed * 0.01f)));
    }

    public void TakeDamage(float damage)
    {
    }
}
