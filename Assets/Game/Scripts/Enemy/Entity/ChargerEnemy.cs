using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChargerEnemy : Enemy
{
    private bool isCharging = false;
    private float enableChargeDis = 8f;

    protected override void Skill()
    {
        isCharging = true;
        Vector3 targetPos = transform.position + transform.right * 10;
        moveSpeed = 0;
        transform.
        transform.DOMove(targetPos, 1).SetDelay(0.5f).OnComplete(() =>
        {
            moveSpeed = enemyData.moveSpeed;
            isCharging = false;
        });
    }

    protected override void MoveToPlayer()
    {
        if(isCharging) return;
        //检查与玩家的距离，到了冲刺距离则停止移动，开始冲刺
        if (distanceToPlayer < enableChargeDis)
        {
            Skill();
        }
        else
        {
            base.MoveToPlayer();
        }
    }
}
