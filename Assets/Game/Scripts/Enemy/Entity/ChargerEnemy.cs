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
        targetPos.x = Mathf.Clamp(targetPos.x,-GlobalVar.mapWidth,GlobalVar.mapWidth);
        targetPos.y = Mathf.Clamp(targetPos.y,-GlobalVar.mapHeight,GlobalVar.mapHeight);
        MoveSpeed = 0;
        transform.
        transform.DOMove(targetPos, 1).SetDelay(0.5f).OnComplete(() =>
        {
            MoveSpeed = enemyData.moveSpeed;
            isCharging = false;
        });
    }

    protected override void HandleMovement()
    {
        if(isCharging) return;
        //检查与玩家的距离，到了冲刺距离则停止移动，开始冲刺
        if (EyreUtility.DistanceCompare2D(distanceToPlayerSq,enableChargeDis,eCompareSign.Less))
        {
            Skill();
        }
        else
        {
            base.HandleMovement();
        }
    }
}
